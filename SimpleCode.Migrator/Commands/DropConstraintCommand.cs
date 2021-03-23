using System.Collections.Generic;

namespace SimpleCode.Migrator.Commands
{
    public class DropConstraintCommand : CommandBase
    {
        private readonly string schema;
        private readonly string table;
        private readonly string constraint;

        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();

        public DropConstraintCommand(string schema, string table, string constraint)
        {
            this.schema = schema;
            this.table = table;
            this.constraint = constraint;
        }
        public override void Execute(ExecutionContext executionContext)
        {
            foreach (var command in ValidationCommands)
                if (command.Validate(executionContext))
                    return;

            string rawSql = $"ALTER TABLE [{this.schema}].[{this.table}] DROP CONSTRAINT [{this.constraint}];";
            executionContext.Execute(rawSql);

        }
    }
}
