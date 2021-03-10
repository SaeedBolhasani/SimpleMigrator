using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DornaDbMigrator
{
    [MigrationMetadata("3.2.2", "13960502")]
    public class FirstMigration:DbMigratorBase
    {
        public FirstMigration()
        {
            this.Table("Sec_Role")
                .InsertIfNotExist("Id", "Name", "Grantable").Values("D8AA3B81-00E1-4B93-B1C3-C3051A6CC8C6", "LocalAdmins", 0)
                .Alter().AddColumn("LastPasswordChangeDateTime", SqlDbType.DateTime2)
                .Update("IsDeleted", 0).Where("[IsDeleted] is null");

        }
    }
}
