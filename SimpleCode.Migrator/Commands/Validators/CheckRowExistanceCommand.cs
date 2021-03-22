using Dapper;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SimpleMigrator.DbMigratorEngine.Commands.Validators
{
    public class CheckRowExistanceCommand : ValidationCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string[] columns;
        private readonly object[] values;

        public CheckRowExistanceCommand(string schema, string table, string[] columns, object[] values)
        {
            this.schema = schema;
            this.table = table;
            this.columns = columns;
            this.values = values;
        }
        public override bool Validate(ExecutionContext executionContext)
        {
            var rawSql = $"Select {string.Join(",", columns)} From [{schema}].[{table}] where {string.Join(" And ", columns.Select((i, j) => i + " = @" + i))};";

            var values = new ExpandoObject() as IDictionary<string, object>;
            for (int i = 0; i < columns.Length; i++)
            {
                values.Add(columns[i], this.values[i]);
            }

            var row = executionContext.SqlConnection.QueryFirstOrDefault(rawSql, values, transaction: executionContext.Transaction);

            return row == null;

        }
    }
}
