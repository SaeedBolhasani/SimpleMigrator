using System;
using SimpleCode.Migrator.Commands;
using SimpleCode.Migrator.Commands.Validators;

namespace SimpleCode.Migrator.Migrators
{
    public class Table
    {
        private readonly DbMigratorBase dbMigrator;
        public string Name { get; }

        public Table(DbMigratorBase dbMigrator, string name)
        {
            this.dbMigrator = dbMigrator;
            Name = name;
        }
        public Table Alter(Action<AlterColumnCollection> expression)
        {
            var alterColumnCollection = new AlterColumnCollection(this);
            expression(alterColumnCollection);
            return this;
        }
        public Alter Alter()
        {
            return new Alter(this);
        }
        public Insert Insert(params string[] columns)
        {
            return new Insert(this, columns);
        }

        public Insert InsertIfNotExist(params string[] columns)
        {
            return new Insert(this, columns, true);
        }

        public Update Update(string column, object value)
        {
            return new Update(this, column, value);
        }

        public Table Create(Action<CreateColumnCollection> columns)
        {
            var alterColumnCollection = new CreateColumnCollection();
            columns(alterColumnCollection);
            this.AddCommand(new CreateTableCommand("dbo", this.Name, alterColumnCollection.Columns));
            return this;
        }

        public Table CreateIfNotExist(Action<CreateColumnCollection> columns)
        {
            var alterColumnCollection = new CreateColumnCollection();
            columns(alterColumnCollection);
            var command = new CreateTableCommand("dbo", this.Name, alterColumnCollection.Columns);
            command.ValidationCommands.Add(new CheckTableExistanceCommand(this.Name));
            this.AddCommand(command);
            return this;
        }

        public void AddCommand(CommandBase command)
        {
            dbMigrator.Commands.Add(command);
        }
    }

    class Create
    {
    }
}