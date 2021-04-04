using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class DropColumnCommand : ISqlCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string column;

        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();
        public IDictionary<string, object> CommandSetions { get; }

        public DropColumnCommand(string schema, string table, string column)
        {
            this.schema = schema;
            this.table = table;
            this.column = column;
        }
        public void Execute(ExecutionContext executionContext)
        {
            foreach (var command in ValidationCommands)
                if (command.Validate(executionContext))
                    return;

            string rawSql = $"ALTER TABLE [{this.schema}].[{this.table}] DROP COLUMN [{this.column}];";
            executionContext.Execute(rawSql);

        }
    }
}
