namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public interface ISchema
    {
        public string Name { get; }
        DbMigratorBase DbMigratorBase { get; }
        ITable Table(string name);
    }
}
