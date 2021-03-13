using SimpleMigrator.DbMigratorEngine;
using System;
using System.Configuration;
using System.Reflection;

namespace SimpleMigrator.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
                throw new Exception();
            var version = args[0];
            var conncetionString = args[1];
            var runner = new MigrationRunner();
            runner.Run(conncetionString, Assembly.GetExecutingAssembly(), version);
        }
    }


}
