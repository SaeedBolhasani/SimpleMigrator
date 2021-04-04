using SimpleMigrator.DbMigratorEngine.Commands;
using System;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{

    public class Alter : IAddColumn, IAlter
    {
        public ITable Table { get; }

        public Alter(ITable table)
        {
            this.Table = table;
        }
    }
}
