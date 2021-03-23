namespace SimpleCode.Migrator.Commands
{
    public abstract class ValidationCommand
    {
        public abstract bool Validate(ExecutionContext executionContext);
    }
}
