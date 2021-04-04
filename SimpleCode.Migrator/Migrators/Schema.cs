namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public class Schema : ISchema
    {
        public DbMigratorBase DbMigratorBase { get; }
        public string Name { get; }

        public Schema(DbMigratorBase dbMigratorBase, string name)
        {
            this.DbMigratorBase = dbMigratorBase;
            this.Name = name;
        }

        public ITable Table(string name)
        {
            return new Table(this, name);
        }
    }
}
