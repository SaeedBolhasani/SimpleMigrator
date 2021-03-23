using System;

namespace SimpleMigrator.Migration
{
    public class SetConnectionString : ICommand
    {
        public string ShortOption { get; } = "c";
        public string Option { get; } = "connection-string";
        public bool CaseSensitive { get; } = false;
        public string Help { get; } = "Use to set connection string.";

        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager)
        {
            if (!tokenManager.HasToken())
                throw new ArgumentException("No value provided for -c|--connectionString");
            var token = tokenManager.GetToken();
            tokenManager.Consume();

            if (token.Type != TokenType.String)
                throw new ArgumentException("No valid value provided for -c|--connectionString");

            migratorConfiguration.ConnectionString = token.Value.ToString();
        }
    }
}
