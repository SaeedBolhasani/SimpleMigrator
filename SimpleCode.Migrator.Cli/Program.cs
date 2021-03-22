using McMaster.NETCore.Plugins;
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

            Console.WriteLine(string.Join(',', args));

            try
            {
                var config = Run(args);

                var asl = new AssemblyLoadContext("s");
                //var asm = asl.LoadFromAssemblyPath(@"D:\Projects\DornaDbMigrator\SimpleMigrator\ConsoleApp1\bin\Debug\netcoreapp3.1\ConsoleApp1.dll");

                var path = @"D:\Projects\DornaDbMigrator\SimpleMigrator\ConsoleApp1\bin\Debug\netcoreapp3.1";
                //var a = Assembly.LoadFrom(path);

                var loaders = new List<PluginLoader>();
                foreach (var file in Directory.GetFiles(path, "*.dll"))
                {
                    var loader = PluginLoader.CreateFromAssemblyFile(file, new[] { typeof(DbMigratorBase) });
                    loaders.Add(loader);

                }

                var aa = loaders.Select(i => i.LoadDefaultAssembly()).SelectMany(i => i.GetTypes()).ToArray();

                AppDomain.CreateDomain("");
                var runner = new MigrationRunner();
                runner.Run("", loaders.Select(i => i.LoadDefaultAssembly()).ToArray(), null);


                //var type = asm.GetExportedTypes();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            // Create the second AppDomain.



            //if (args == null || args.Length != 2)
            //    throw new Exception();
            //var version = args[0];
            //var conncetionString = args[2];

        }
        //public class AssemblyLoader : AssemblyLoadContext
        //{
        //    Not exactly sure about this
        //    protected override Assembly Load(AssemblyName assemblyName)
        //    {
        //        var deps = DependencyContext.Default;
        //        var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
        //        var assembly = Assembly.Load(new Asse));
        //        return assembly;
        //    }
        //}
        private static MigratorConfiguration Run(string[] args)
        {
            var tokenizer = new Tokenizer();

            var tokens = tokenizer.Tokenize(args);

            var tokenManager = new TokenManager(tokens);

            var commands = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(i => typeof(ICommand).IsAssignableFrom(i) && !i.IsInterface)
                .Select(i => Activator.CreateInstance(i))
                .Cast<ICommand>()
                .ToArray();
            var configuration = new MigratorConfiguration();
            while (tokenManager.HasToken())
            {
                var currentToken = tokenManager.GetToken();
                tokenManager.Consume();

                if (currentToken.Type != TokenType.Switch)
                    throw new ArgumentException($"Invalid switch {currentToken.RawValue}");

                var command = commands.FirstOrDefault(i => string.Equals(i.ShortOption, (string)currentToken.Value, i.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase));

                if (command == null)
                    throw new ArgumentException($"Unkown switch {currentToken.RawValue}");

                command.Execute(configuration, tokenManager);
            }
            return configuration;
        }


    }
}
