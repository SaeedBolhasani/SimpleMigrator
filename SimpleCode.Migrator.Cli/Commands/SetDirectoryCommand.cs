using System;
using System.IO;

namespace SimpleMigrator.Migration.Commands
{
    public class SetDirectoryCommand : ICommand
    {
        public bool CaseSensitive { get; } = false;
        public string ShortOption { get; } = "d";
        public string Option { get; } = "directory";
        public string Help { get; } = "Use for set directory that assembly file exist.";

        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager)
        {
            if (!tokenManager.HasToken())
                throw new ArgumentException("No value provided for -d|--directory");
            var token = tokenManager.GetToken();
            tokenManager.Consume();

            if (token.Type != TokenType.String)
                throw new ArgumentException("No valid value provided for -d|--directory");

            var dir = token.Value.ToString();
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Directory '{dir}' does not exist.");

            migratorConfiguration.Directory = dir;
        }
    }
}
