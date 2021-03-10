using DornaDbMigrator.Commands;
using System;

namespace DornaDbMigrator
{
    public class Insert
    {
        private readonly Table table;
        private readonly string[] columns;
        private readonly bool ifNotExist;

        public Insert(Table table, string[] columns) : this(table, columns, false) { }
        public Insert(Table table, string[] columns,bool ifNotExist)
        {
            this.table = table;
            this.columns = columns;
            this.ifNotExist = ifNotExist;
        }
        public Table Values(params object[] values)
        {
            if (values?.Length != this.columns?.Length)
                throw new ArgumentException(nameof(values));

            if(this.ifNotExist)
                this.table.AddCommand(new CheckExistanceCommand("dbo", this.table.Name, this.columns, values));


            this.table.AddCommand(new InsertCommand("dbo", this.table.Name, this.columns, values));

            return this.table;
        }
    }
}
