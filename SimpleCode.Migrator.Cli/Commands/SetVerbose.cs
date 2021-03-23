using SimpleCode.Migrator.Cli.Interfaces;
using SimpleCode.Migrator.Cli.TokenRelated;

namespace SimpleCode.Migrator.Cli.Commands
{
    public class SetVerbose : ICommand
    {
        public string ShortOption { get; } = "V";
        public string Option { get; } = "Verbose";
        public bool CaseSensitive { get; } = false;
        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager)
        {

            if (tokenManager.HasToken())
            {
                var token = tokenManager.GetToken();

                if (token.Type == TokenType.Boolean)
                {
                    migratorConfiguration.ShowVerbose = (bool)token.Value;
                    tokenManager.Consume();
                }
                else
                    migratorConfiguration.ShowVerbose = true;
            }
            else
            {
                migratorConfiguration.ShowVerbose = true;
            }
        }
    }
}
