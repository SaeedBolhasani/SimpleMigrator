namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public abstract class CommandBase
    {
        public abstract void Execute(ExecutionContext executionContext);
    }
}
