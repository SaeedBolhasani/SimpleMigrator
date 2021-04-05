using SimpleCode.Migrator.Migrators.Extensions;
using SimpleCode.Migrator.SqlServer;
using SimpleMigrator.DbMigratorEngine;
using SimpleMigrator.DbMigratorEngine.Migrators;
using System;
using System.Data;
using System.Linq;

namespace ConsoleApp2
{
    [MigrationMetadata("1.0.0.0", "13960000")]
    public class FirstMigration : DbMigratorBase
    {
        public FirstMigration()
        {

            this.Schema("dbo")
                .Table("mytest")
                .Alter().AddColumn(i => i.Name("Column3").Type("numeric").NotNull())
                        .ModifyColumn(i => i.Name("Column2").Type("nvarchar(10)").NotNull());//.IfNotExist();

        }
    }

  
   
}
