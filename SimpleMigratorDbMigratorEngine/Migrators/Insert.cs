using SimpleMigrator.DbMigratorEngine.Commands;
using SimpleMigrator.DbMigratorEngine.Commands.Validators;
using System;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public class Insert
    {
        private readonly Table table;
        private readonly string[] columns;
        private readonly bool ifNotExist;

        public Insert(Table table, string[] columns) : this(table, columns, false) { }
        public Insert(Table table, string[] columns, bool ifNotExist)
        {
            this.table = table;
            this.columns = columns;
            this.ifNotExist = ifNotExist;
        }
        public Table Values(params object[] values)
        {
            if (values?.Length != columns?.Length)
                throw new ArgumentException(nameof(values));

            var command = new InsertCommand("dbo", table.Name, columns, values);

            if (ifNotExist)
                command.ValidationCommands.Add(new CheckRowExistanceCommand("dbo", table.Name, columns, values));


            table.AddCommand(command);

            return table;
        }
    }
}
