using Dapper;

namespace SimpleCode.Migrator.Commands.Validators
{
    public class CheckConstraintExistanceCommand : ValidationCommand
    {
        private readonly string table;
        private readonly string constraintName;

        public CheckConstraintExistanceCommand(string table, string constraintName)
        {
            this.table = table;
            this.constraintName = constraintName;
        }
        public override bool Validate(ExecutionContext executionContext)
        {
            var rawSql = $@"SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME = @Table and CONSTRAINT_NAME=@Column";
            var row = executionContext.SqlConnection.QueryFirstOrDefault(rawSql, new { Table = table, Column = constraintName }, transaction: executionContext.Transaction);

            return row == null;
        }
    }
}
