using System.Collections.Generic;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class UpdateCommand : ISqlCommand
    {
        private readonly string schema;
        private readonly string table;
        private readonly string column;
        private readonly object value;
        private readonly string condition;

        public UpdateCommand(string schema, string table, string column, object value, string condition)
        {
            this.schema = schema;
            this.table = table;
            this.column = column;
            this.value = value;
            this.condition = condition;
        }

        public IDictionary<string, object> CommandSetions { get; }

        public void Execute(ExecutionContext connection)
        {
            string rawSql = $"update [{schema}].[{table}] set [{column}] = {value} where {condition}";
            connection.Execute(rawSql);
        }
    }
}
