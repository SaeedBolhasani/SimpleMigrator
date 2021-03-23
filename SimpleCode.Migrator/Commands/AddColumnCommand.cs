using System.Collections.Generic;
using System.Text;
using SimpleCode.Migrator.Models;

namespace SimpleCode.Migrator.Commands
{
    public class AddColumnCommand : CommandBase
    {
        private readonly string schema;
        private readonly string table;
        private readonly string column;
        private readonly string type;
        private readonly bool allowNull;
        private readonly string constraint;
        private readonly object defaultValue;
        private readonly Identity identity;

        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();

        public AddColumnCommand(string schema,
            string table,
            string column,
            string type,
            bool allowNull,
            string constraint,
            object defaultValue,
            Identity identity)
        {
            this.schema = schema;
            this.table = table;
            this.column = column;
            this.type = type;
            this.allowNull = allowNull;
            this.constraint = constraint;
            this.defaultValue = defaultValue;
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

            if (defaultValue != null)
                stringBuilder.AppendFormat("DEFAULT (({0})) ", defaultValue);

            if (identity != null)
                stringBuilder.Append($"IDENTITY({identity.Seed},{identity.Increment}) ");

            var sql = stringBuilder.ToString();
            executionContext.Execute(sql);
        }
    }
}
