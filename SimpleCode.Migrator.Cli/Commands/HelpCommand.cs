using SimpleMigrator.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCode.Migrator.Cli.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly IEnumerable<ICommand> commands;

        public HelpCommand(IEnumerable<ICommand> commands)
        {
            this.commands = commands;
        }
        public bool CaseSensitive { get; } = false;
        public string ShortOption { get; } = "h";
        public string Option { get; } = "help";
        public string Help { get; }

        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager)
        {
            foreach (var command in this.commands)
            {
                Console.Write($"-{command.ShortOption}|--{command.Option}".PadRight(50, ' '));
                Console.WriteLine(command.Help);
            }
        }
    }
}
