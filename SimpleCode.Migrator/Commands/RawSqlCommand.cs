using System.Collections.Generic;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class RawSqlCommand : ISqlCommand
    {
        private readonly string rawSql;
        private readonly object values;

        public RawSqlCommand(string rawSql, object values)
        {
            this.rawSql = rawSql;
            this.values = values;
        }

        public IDictionary<string, object> CommandSetions { get; }

        public void Execute(ExecutionContext connection)
        {
            connection.Execute(rawSql, values);
        }
    }
}
