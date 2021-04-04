using SimpleMigrator.DbMigratorEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public class AddColumnCommand : ISqlCommand
    {
        public List<ValidationCommand> ValidationCommands { get; } = new List<ValidationCommand>();
        public IDictionary<string, object> CommandSetions { get; } = new KeyInsensitiveDictionray<object>();

        public void Execute(ExecutionContext executionContext)
        {
            foreach (var command in ValidationCommands)
                if (!command.Validate(executionContext))
                    return;

            var sql = string.Join(' ', CommandSetions.Select(i => i.Value.ToString().Trim()));
            executionContext.Execute(sql);
        }
    }

    public class KeyInsensitiveDictionray<T>:Dictionary<string,T>,IDictionary<string,T>
    {
        public new void Add(string key,T value)
        {
            base.Add(key.ToLower(), value);
        }
        public new T this[string key]
        {
            get => base[key.ToLower()];
            set => base[key.ToLower()] = value;
        }

    }
}
