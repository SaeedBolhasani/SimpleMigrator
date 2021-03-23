using System.Data;
using SimpleCode.Migrator.Migrators;
using SimpleCode.Migrator.Models;

namespace SimpleCode.Migrator.Migrations
{
    public class Initial : DbMigratorBase
    {
        public Initial()
        {
            this.Table("MigrationsHistory")
                .CreateIfNotExist(i => {
                    i.AddColumn("Id", SqlDbType.Int, allowNull: false, identity: new Identity(1, 1));
                    i.AddColumn("Version","nvarchar(10)", allowNull: false);
                    i.AddColumn("Date","nvarchar(10)", allowNull: false);
                });
        }
    }
}
