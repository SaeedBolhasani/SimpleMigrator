using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DornaDbMigrator
{
    public class AddColumnCommand : ExecuteCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string column;
        private readonly SqlDbType type;
        private readonly bool allowNull;
        private readonly string constraint;
        private readonly object defalutValue;

        public AddColumnCommand(string schema,
            string table,
            string column,
            SqlDbType type,
            bool allowNull,
            string constraint,
            object defalutValue)
        {
            this.schema = schema;
            this.table = table;
            this.column = column;
            this.type = type;
            this.allowNull = allowNull;
            this.constraint = constraint;
            this.defalutValue = defalutValue;
        }
        public override void Execute(SqlConnection connection)
        {
            var sql = $@"alter table [{this.schema}].[{this.table}] 
                         add [{this.column}] [{this.type}] 
                        {(this.allowNull ? "Null" : "Not Null")} 
                        {(constraint != null ? $"CONSTRAINT [{constraint}]" : "")} 
                        {(this.defalutValue != null ? $"DEFAULT (({defalutValue}))" : "")};";

            var effectedRaw = connection.Execute(sql);
            Console.WriteLine(sql + $" effected raw: {effectedRaw}");
        }
    }
}
