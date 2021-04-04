namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public static class Extensions
    {
        public static ISchema Schema(this DbMigratorBase dbMigratorBase, string name)
        {
            return new Schema(dbMigratorBase, name);
        }
    }
}
