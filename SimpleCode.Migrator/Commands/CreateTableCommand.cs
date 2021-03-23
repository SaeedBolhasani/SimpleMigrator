﻿using System.Collections.Generic;

namespace SimpleCode.Migrator.Commands
{
    public class CreateTableCommand:CommandBase
    {
        private readonly string schema;
        private readonly string table;
        private readonly List<string> columns;
        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();

        public CreateTableCommand(string schema, string table, List<string> columns)
        {
            this.schema = schema;
            this.table = table;
            this.columns = columns;
        }
        public override void Execute(ExecutionContext connection)
        {
            foreach (var command in ValidationCommands)
                if (!command.Validate(connection))
                    return;

            string rawSql = $"CREATE TABLE [{this.schema}].[{this.table}]({string.Join(",", this.columns)});";
            connection.Execute(rawSql);
        }
    }
}
