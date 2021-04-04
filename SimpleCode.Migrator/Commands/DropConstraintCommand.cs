using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class DropConstraintCommand : ISqlCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string constraint;

        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();
        public IDictionary<string, object> CommandSetions { get; }

        public DropConstraintCommand(string schema, string table, string constraint)
        {
            this.schema = schema;
            this.table = table;
            this.constraint = constraint;
        }
        public void Execute(ExecutionContext executionContext)
        {
            foreach (var command in ValidationCommands)
                if (command.Validate(executionContext))
                    return;

            string rawSql = $"ALTER TABLE [{this.schema}].[{this.table}] DROP CONSTRAINT [{this.constraint}];";
            executionContext.Execute(rawSql);

        }
    }
}
