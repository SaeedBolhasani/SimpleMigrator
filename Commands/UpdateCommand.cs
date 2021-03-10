using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;

namespace DornaDbMigrator
{
    public class UpdateCommand : ExecuteCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string column;
        private readonly object value;
        private readonly string condition;

        public UpdateCommand(string schema, string table, string column, object value,string condition)
        {
            this.schema = schema;
            this.table = table;
            this.column = column;
            this.value = value;
            this.condition = condition;
        }
        public override void Execute(SqlConnection connection)
        {
            string rawSql = $"update [{this.schema}].[{this.table}] set [{this.column}] = {this.value} where {condition}";
            var effectedRaw = connection.Execute(rawSql);
            Console.WriteLine(rawSql + $" effected raw: {effectedRaw}");
        }
    }
}
