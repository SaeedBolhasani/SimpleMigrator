using McMaster.NETCore.Plugins;
using Microsoft.Extensions.DependencyInjection;
using SimpleCode.Migrator.Cli.Commands;
using SimpleMigrator.DbMigratorEngine;
using SimpleMigrator.DbMigratorEngine.Migrators;
using SimpleMigrator.Migration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SimpleCode.Migrator.Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                    args = new[] { "-h" };

                var config = Run(args);
                if (config.AssemblyPath == null && config.Directory == null)
                    throw new Exception($"Both assembly path and directory can not be empty together.");

                var files = new List<string>();

                if (config.AssemblyPath != null)
                    files.Add(config.AssemblyPath);

                if (config.Directory != null)
                    files.AddRange(Directory.GetFiles(config.Directory, "*.dll"));

                var loaders = new List<PluginLoader>();
                foreach (var file in files)
                {
                    var loader = PluginLoader.CreateFromAssemblyFile(file, new[] { typeof(DbMigratorBase) });
                    loaders.Add(loader);
                }

                var runner = new MigrationRunner();

                runner.Run(config.ConnectionString, loaders.Select(i => i.LoadDefaultAssembly()).ToArray(), null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static MigratorConfiguration Run(string[] args)
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(args);
            var tokenManager = new TokenManager(tokens);

            IEnumerable<ICommand> commands = GetAllCommands();

            var configuration = new MigratorConfiguration();
            while (tokenManager.HasToken())
            {
                var currentToken = tokenManager.GetToken();
                tokenManager.Consume();

                if (currentToken.Type != TokenType.Switch)
                    throw new ArgumentException($"Invalid switch {currentToken.RawValue}");

                var command = commands.FirstOrDefault(i => string.Equals(i.ShortOption, (string)currentToken.Value, i.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase));
                if (command == null)
                    command = commands.FirstOrDefault(i => string.Equals(i.Option, (string)currentToken.Value, i.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase));

                if (command == null)
                    throw new ArgumentException($"Unkown switch {currentToken.RawValue}");

                command.Execute(configuration, tokenManager);
            }
            return configuration;
        }

        private static IEnumerable<ICommand> GetAllCommands()
        {
            var serviceCollection = new ServiceCollection();

            var commandTypes = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(i => typeof(ICommand).IsAssignableFrom(i) && !i.IsInterface && i != typeof(HelpCommand))
                .ToArray();

            foreach (var commandType in commandTypes)
            {
                serviceCollection.AddSingleton(typeof(ICommand), commandType);
            }

            serviceCollection.AddSingleton<HelpCommand>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var helpCommand = serviceProvider.GetService<HelpCommand>() as ICommand;

            var commands = serviceProvider.GetServices<ICommand>().Concat(new[] { helpCommand });
            return commands;
        }
    }
}
