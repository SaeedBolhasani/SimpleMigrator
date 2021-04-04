namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public interface IColumnOption
    {
        IColumnOption Constraint(string constraint);

        IColumnOption Null();

        IColumnOption NotNull();
       
    }
}
