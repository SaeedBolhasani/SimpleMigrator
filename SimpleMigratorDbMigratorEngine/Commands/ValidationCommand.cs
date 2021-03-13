namespace SimpleMigrator.DbMigratorEngine.Commands
{
    public abstract class ValidationCommand
    {
        public abstract bool Validate(ExecutionContext executionContext);
    }
}
