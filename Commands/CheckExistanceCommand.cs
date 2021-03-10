using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace DornaDbMigrator.Commands
{
    public class CheckExistanceCommand : ValidationCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string[] columns;
        private readonly object[] values;

        public CheckExistanceCommand(string schema, string table, string[] columns, object[] values)
        {
            this.schema = schema;
            this.table = table;
            this.columns = columns;
            this.values = values;
        }
        public override bool Validate(SqlConnection connection)
        {
            var rawSql = $"Select {string.Join(",", columns)} From [{this.schema}].[{this.table}] where {string.Join(" And ", columns.Select((i, j) => i + " = @" + i))};";

            var values = new ExpandoObject() as IDictionary<string, object>;
            for (int i = 0; i < this.columns.Length; i++)
            {
                values.Add(columns[i], this.values[i]);
            }

            var row = connection.QueryFirstOrDefault(rawSql, values);

            return row != null;
            
        }
    }
}
