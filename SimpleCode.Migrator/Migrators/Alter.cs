using SimpleMigrator.DbMigratorEngine.Commands;
using SimpleMigrator.DbMigratorEngine.Commands.Validators;
using SimpleMigrator.DbMigratorEngine.Models;
using System;
using System.Data;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public class Alter
    {
        private readonly Table table;

        public Alter(Table table)
        {
            this.table = table;
        }
        public Table AddColumn(string columnName, SqlDbType type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            var command = new AddColumnCommand("dbo", table.Name, columnName, type.ToString(), allowNull, constraint, defaultValue, identity);
            table.AddCommand(command);
            return table;
        }
        public Table AddColumnIfNoExist(string columnName, SqlDbType type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            var command = new AddColumnCommand("dbo", table.Name, columnName, type.ToString(), allowNull, constraint, defaultValue,identity);
            command.ValidationCommands.Add(new CheckColumnExistanceCommand(table.Name, columnName));

            if (constraint != null)
            {
                command.ValidationCommands.Add(new CheckConstraintExistanceCommand(table.Name, constraint));
            }

            table.AddCommand(command);
            return table;
        }
        public Table AddColumnIfNoExist(string columnName, string type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            var command = new AddColumnCommand("dbo", table.Name, columnName, type, allowNull, constraint, defaultValue,identity);
            command.ValidationCommands.Add(new CheckColumnExistanceCommand(table.Name, columnName));
            table.AddCommand(command);
            return table;
        }
        public Table DropColumn(string columnName)
        {
            var command = new DropColumnCommand("dbo", table.Name, columnName);
            table.AddCommand(command);
            return table;
        }

        public Table DropColumnIfNotExist(string columnName)
        {
            var command = new DropColumnCommand("dbo", table.Name, columnName);
            command.ValidationCommands.Add(new CheckColumnExistanceCommand(this.table.Name, columnName));
            table.AddCommand(command);
            return table;
        }
        public Table DropConstraint(string constraint)
        {
            var command = new DropConstraintCommand("dbo", table.Name, constraint);
            table.AddCommand(command);
            return table;
        }

        public Table DropConstraintIfNotExists(string constraint)
        {
            var command = new DropConstraintCommand("dbo", table.Name, constraint);
            command.ValidationCommands.Add(new CheckConstraintExistanceCommand(this.table.Name, constraint));
            table.AddCommand(command);
            return table;
        }

    }
}
