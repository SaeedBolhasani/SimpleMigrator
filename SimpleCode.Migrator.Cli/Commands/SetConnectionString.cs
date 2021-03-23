using System;
using SimpleCode.Migrator.Cli.Interfaces;
using SimpleCode.Migrator.Cli.TokenRelated;

namespace SimpleCode.Migrator.Cli.Commands
{
    public class SetConnectionString : ICommand
    {
        public string ShortOption { get; } = "C";
        public string Option { get; } = "ConnectionString";
        public bool CaseSensitive { get; } = false;
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
