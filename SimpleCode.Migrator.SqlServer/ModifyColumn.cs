using SimpleMigrator.DbMigratorEngine.Migrators;

namespace SimpleCode.Migrator.SqlServer
{
    public class ModifyColumn : Alter, IModifyColumn
    {
        public ModifyColumn(ITable table) : base(table)
        {
        }
    }
}
