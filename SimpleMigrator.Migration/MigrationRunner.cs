using Dapper;
using SimpleMigrator.DbMigratorEngine;
using SimpleMigrator.DbMigratorEngine.Commands;
using SimpleMigrator.DbMigratorEngine.Migrations;
using SimpleMigrator.DbMigratorEngine.Migrators;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SimpleMigrator.Migration
{
    public class MigrationRunner
    {
        public void Run(string connectionString, Assembly[] assemblies, string version = null)
        {
            var migratorTypes = assemblies.SelectMany(i => i.GetExportedTypes())
               .Where(i => typeof(DbMigratorBase).IsAssignableFrom(i) && !i.IsAbstract)
               .Where(i => i.GetCustomAttribute<MigrationMetadataAttribute>() != null).ToArray();

            if (version != null)
                migratorTypes = migratorTypes.Where(i => i.GetCustomAttribute<MigrationMetadataAttribute>().SimpleMigratorVersion.CompareTo(version) < 1).ToArray();

            var migrators = migratorTypes.OrderBy(i => i.GetCustomAttribute<MigrationMetadataAttribute>().SimpleMigratorVersion)
               .Select(i => Activator.CreateInstance(i))
               .Cast<DbMigratorBase>()
               .ToArray();

            if (migrators.Length == 0)
                return;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var exec = new ExecutionContext
                {
                    SqlConnection = connection,
                };
                var transaction = connection.BeginTransaction();
                exec.Transaction = transaction;
                try
                {
                    CheckMigrationHistoryTable(exec);
                    var lastHistory = GetLatestMigrationHistory(exec);
                    var lastMigration = migrators.Last().GetType().GetCustomAttribute<MigrationMetadataAttribute>();
                    if (lastHistory != null && lastHistory.Version.CompareTo(lastMigration.SimpleMigratorVersion) > -1)
                    {
                        Console.WriteLine("Your database is already update.");
                        connection.Dispose();
                        transaction.Dispose();
                        return;
                    }

                    foreach (var migrator in migrators)
                    {
                        foreach (var item in migrator.Commands)
                        {
                            item.Execute(exec);
                        }
                        var migrationMetadate = migrator.GetType().GetCustomAttribute<MigrationMetadataAttribute>();
                        InsertMigrationHistory(exec, migrationMetadate.SimpleMigratorVersion, migrationMetadate.Date);
                    }
                    transaction.Commit();
                }
                catch (SqlException e)
                {
                    transaction.Rollback();
                    //throw;
                    Console.WriteLine(e.Message);

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            Console.WriteLine("Press any key to exit ... ");
            Console.ReadKey();
        }

        private void CheckMigrationHistoryTable(ExecutionContext executionContext)
        {
            var initialMigration = new Initial();
            initialMigration.Commands.ForEach(i => i.Execute(executionContext));
        }
        private void InsertMigrationHistory(ExecutionContext executionContext, string version, string date)
        {
            var insertCommand = new InsertCommand("dbo", "MigrationsHistory", new[] { "Version", "Date" }, new[] { version, date });
            insertCommand.Execute(executionContext);

        }

        private MigrationHistory GetLatestMigrationHistory(ExecutionContext executionContext)
        {
            return executionContext.SqlConnection.Query<MigrationHistory>(new CommandDefinition("SELECT * FROM dbo.MigrationsHistory", transaction: executionContext.Transaction)).LastOrDefault();
        }

        public class MigrationHistory
        {
            public int Id { get; set; }
            public string Version { get; set; }
            public string Date { get; set; }
        }
    }

}
