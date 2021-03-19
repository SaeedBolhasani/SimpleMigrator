namespace SimpleMigrator.Migration
{
    public interface ICommand
    {
        public bool CaseSensitive { get; }
        public string ShortOption { get; }
        public string Option { get; }
        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager);
    }
}
