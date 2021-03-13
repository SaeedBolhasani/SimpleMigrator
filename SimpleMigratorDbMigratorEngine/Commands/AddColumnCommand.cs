using SimpleMigrator.DbMigratorEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class AddColumnCommand : CommandBase
    {
        private readonly string schema;
        private readonly string table;
        private readonly string column;
        private readonly string type;
        private readonly bool allowNull;
        private readonly string constraint;
        private readonly object defalutValue;
        private readonly Identity identity;

        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();

        public AddColumnCommand(string schema,
            string table,
            string column,
            string type,
            bool allowNull,
            string constraint,
            object defalutValue,
            Identity identity)
        {
            this.schema = schema;
            this.table = table;
            this.column = column;
            this.type = type;
            this.allowNull = allowNull;
            this.constraint = constraint;
            this.defalutValue = defalutValue;
            this.identity = identity;
        }
        public override void Execute(ExecutionContext executionContext)
        {
            foreach (var command in ValidationCommands)
                if (!command.Validate(executionContext))
                    return;
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("alter table [{0}].[{1}] add[{2}] {3} ",
                schema, table, column, type);

            stringBuilder.Append(allowNull ? "Null " : "Not Null ");

            if (constraint != null)
                stringBuilder.AppendFormat("CONSTRAINT [{0}] ", constraint);

            if (defalutValue != null)
                stringBuilder.AppendFormat("DEFAULT (({0})) ", defalutValue);

            if (identity != null)
                stringBuilder.Append($"IDENTITY({identity.Seed},{identity.Increament}) ");

            var sql = stringBuilder.ToString();
            executionContext.Execute(sql);
        }
    }
}
