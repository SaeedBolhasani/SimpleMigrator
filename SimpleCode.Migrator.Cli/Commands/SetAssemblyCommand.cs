using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMigrator.Migration.Commands
{
    public class SetAssemblyCommand : ICommand
    {
        public bool CaseSensitive { get; } = false;
        public string ShortOption { get; } = "a";
        public string Option { get; } = "assembly";

        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager)
        {
            if (!tokenManager.HasToken())
                throw new ArgumentException("No value provided for -a|--assembly");
            var token = tokenManager.GetToken();
            tokenManager.Consume();

            if (token.Type != TokenType.String)
                throw new ArgumentException("No valid value provided for -a|--assembly");

            migratorConfiguration.AssemblyPath = token.Value.ToString();
        }
    }
}
