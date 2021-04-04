using System.Collections.Generic;

namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public interface ISqlCommand
    {
        IDictionary<string, object> CommandSetions { get; }
        void Execute(ExecutionContext executionContext);
    }
}
