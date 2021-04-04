namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public interface IColumnType
    {
        IColumnOption Type(string name);
    }
}
