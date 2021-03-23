using Dapper;

namespace SimpleCode.Migrator.Commands.Validators
{
    public class CheckColumnExistanceCommand : ValidationCommand
    {
        private readonly string table;
        private readonly string column;

        public CheckColumnExistanceCommand(string table, string column)
        {
            this.table = table;
            this.column = column;
        }
        public override bool Validate(ExecutionContext executionContext)
        {
            var rawSql = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @Table and COLUMN_NAME=@Column";
            var row = executionContext.SqlConnection.QueryFirstOrDefault(rawSql, new { Table = table, Column = column }, transaction: executionContext.Transaction);

            return row == null;
        }
    }
}
