using SimpleCode.Migrator.Migrators.Extensions;
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
                .Table("Test")
                .Alter().AddColumn(i => i.Name("Column1").Type("T1").NotNull());//.IfNotExist();

        }
    }

  
   
}
