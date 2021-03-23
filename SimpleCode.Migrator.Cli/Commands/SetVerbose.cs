namespace SimpleMigrator.Migration
{
    public class SetVerbose : ICommand
    {
        public string ShortOption { get; } = "v";
        public string Option { get; } = "verbose";
        public bool CaseSensitive { get; } = false;

        public string Help { get; } = "If leave it empty or set true show progress verbosely.";

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
