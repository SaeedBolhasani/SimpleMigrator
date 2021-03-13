using Dapper;

namespace SimpleMigrator.DbMigratorEngine.Commands.Validators
{
    public class CheckTableExistanceCommand : ValidationCommand
    {
        private readonly string table;

        public CheckTableExistanceCommand(string table)
        {
            this.table = table;
        }
        public override bool Validate(ExecutionContext executionContext)
        {
            var rawSql = $@"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @Table";
            var row = executionContext.SqlConnection.QueryFirstOrDefault(rawSql, new { Table = table }, transaction: executionContext.Transaction);

            return row == null;
        }
    }
}
