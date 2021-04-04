using System;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public interface ITable
    {
        public string Name { get; }
        ISchema Schema { get; }
    }
}
