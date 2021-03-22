namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class RawSqlCommand : CommandBase
    {
        private readonly string rawSql;
        private readonly object values;

        public RawSqlCommand(string rawSql, object values)
        {
            this.rawSql = rawSql;
            this.values = values;
        }
        public override void Execute(ExecutionContext connection)
        {
            connection.Execute(rawSql, values);
        }
    }
}
