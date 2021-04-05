using SimpleMigrator.DbMigratorEngine.Migrators;

namespace SimpleMigrator.DbMigratorEngine.Migrations
{
    public class Initial : DbMigratorBase
    {
        public Initial()
        {
            //this.Table("MigrationsHistory")
            //    .CreateIfNotExist(i => {
            //        i.AddColumn("Id", SqlDbType.Int, allowNull: false, identity: new Identity(1, 1));
            //        i.AddColumn("Version","nvarchar(10)", allowNull: false);
            //        i.AddColumn("Date","nvarchar(10)", allowNull: false);
            //    });
        }
    }
}
