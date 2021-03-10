using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;

namespace DornaDbMigrator
{
    public class InsertCommand : ExecuteCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string[] columns;
        private readonly object[] values;

        public InsertCommand(string schema, string table, string[] columns, object[] values)
        {
            this.schema = schema;
            this.table = table;
            this.columns = columns;
            this.values = values;
        }
        public override void Execute(SqlConnection connection)
        {
            string rawSql = $"INSERT INTO [{this.schema}].[{this.table}] ({string.Join(",", columns)}) Values ({string.Join(",", columns.Select(i => "@" + i))});";
            var values = new ExpandoObject() as IDictionary<string, object>;
            for (int i = 0; i < this.columns.Length; i++)
            {
                values.Add(columns[i], this.values[i]);
            }

            var effectedRaw = connection.Execute(rawSql, values);

            Console.WriteLine(rawSql + $" effected raw: {effectedRaw}");

        }
    }
}
