using System;
using SimpleCode.Migrator.Cli.Interfaces;
using SimpleCode.Migrator.Cli.TokenRelated;

namespace SimpleCode.Migrator.Cli.Commands
{
    public class SetDirectoryCommand : ICommand
    {
        public bool CaseSensitive { get; } = false;
        public string ShortOption { get; } = "d";
        public string Option { get; } = "directory";

        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager)
        {
            if (!tokenManager.HasToken())
                throw new ArgumentException("No value provided for -d|--directory");
            var token = tokenManager.GetToken();
            tokenManager.Consume();

            if (token.Type != TokenType.String)
                throw new ArgumentException("No valid value provided for -d|--directory");

            migratorConfiguration.Directory = token.Value.ToString();
        }
    }
}
