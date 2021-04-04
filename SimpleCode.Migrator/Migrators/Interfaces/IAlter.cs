using System;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public interface IAlter
    {
        ITable Table { get; }        
    }
}
