using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SimpleCode.Migrator.Commands
{
    public class InsertCommand : CommandBase
    {
        private readonly string schema;
        private readonly string table;
        private readonly string[] columns;
        private readonly object[] values;

        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();

        public InsertCommand(string schema, string table, string[] columns, object[] values)
        {
            this.schema = schema;
            this.table = table;
            this.columns = columns;
            this.values = values;
        }
        public override void Execute(ExecutionContext executionContext)
        {
            foreach (var command in ValidationCommands)
                if (!command.Validate(executionContext))
                    return;


            string rawSql = $"INSERT INTO [{schema}].[{table}] ({string.Join(",", columns)}) Values ({string.Join(",", columns.Select(i => "@" + i))});";
            var values = new ExpandoObject() as IDictionary<string, object>;
            for (int i = 0; i < columns.Length; i++)
            {
                values.Add(columns[i], this.values[i]);
            }

            executionContext.Execute(rawSql, values);

        }
    }
}
