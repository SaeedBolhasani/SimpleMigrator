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
        /*
        EXEC sp_RENAME '[dbo].[BusinessLog].[Service]' , 'Module', 'COLUMN'
        ----------------------------------------------------------------------------------------
        alter table dbo.sec_user
        add [LastPasswordChangeDateTime] [datetime] NULL,
	        [Lock] [bit] NULL CONSTRAINT [DF_Sec_User_Lock]  DEFAULT ((0)),
	        [InActive] [bit] NULL CONSTRAINT [DF_Sec_User_InActive]  DEFAULT ((0)),
	        [PasswordFailCount] [int] NULL CONSTRAINT [DF_Sec_User_FailCount]  DEFAULT ((0)),
	        [LastLoginDateTime] [datetime] NULL,
	        [LoginDateTime] [datetime] NULL
        ----------------------------------------------------------------------------------------
        update [dbo].[Sec_User] set [LastPasswordChangeDateTime] = GETDATE()
        where [LastPasswordChangeDateTime] is null
        update [dbo].[Sec_User] set [Lock] = 0
        where [Lock] is null
        update [dbo].[Sec_User] set [InActive] = 0
        where [InActive] is null
        update [dbo].[Sec_User] set [PasswordFailCount] = 0
        where [PasswordFailCount] is null
        ----------------------------------------------------------------------------------------
        alter table dbo.Rep_ReportTree
        add [DeleteTime] datetime NULL
        ----------------------------------------------------------------------------------------
        alter table dbo.Rep_ReportTree
        add [Code] nvarchar(200) NULL
        */
        public FirstMigration()
        {
            //this.ExecuteRawSql("EXEC sp_RENAME '[dbo].[BusinessLog].[Service]' , 'Module', 'COLUMN'", null);
            this.Table("Sec_User")
                .Create(i => i.AddColumn("id", SqlDbType.BigInt, identity: new SimpleMigrator.DbMigratorEngine.Models.Identity(1, 1), allowNull: false))
                .Alter(i =>
                {
                    i.AddColumnIfNoExist("LastPasswordChangeDateTime", SqlDbType.DateTime)
                     //.AddColumnIfNoExist("Lock", SqlDbType.Bit, defaultValue: 0, constraint: "DF_Sec_User_Lock")
                     //.AddColumnIfNoExist("InActive", SqlDbType.Bit, defaultValue: 0, constraint: "DF_Sec_User_InActive")
                     .AddColumnIfNoExist("PasswordFailCount", SqlDbType.Int)
                     .AddColumnIfNoExist("LastLoginDateTime", SqlDbType.DateTime)
                     .AddColumnIfNoExist("LoginDateTime", SqlDbType.DateTime);

                })
                .Update("LastPasswordChangeDateTime", $"'{DateTime.Now}'").Where("[LastPasswordChangeDateTime] is null")
                //.Update("Lock", 0).Where("[Lock] is null")
                //.Update("InActive", 0).Where("[InActive] is null")
                .Update("PasswordFailCount", 0).Where("[PasswordFailCount] is null");

            //this.Table("Rep_ReportTree")
            //    .Alter().AddColumnIfNoExist("DeleteTime", SqlDbType.DateTime)
            //    .Alter().AddColumnIfNoExist("Code", "nvarchar(200)");


        }
    }
}
