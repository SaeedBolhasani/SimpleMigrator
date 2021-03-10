using System.Data;

namespace DornaDbMigrator
{
    public class Alter
    {
        private readonly Table table;

        public Alter(Table table)
        {
            this.table = table;
        }
        public Table AddColumn(string columnName, SqlDbType type, bool allowNull = true, object defaultValue = null,string constrain = null)
        {
            this.table.AddCommand(new AddColumnCommand("dbo", this.table.Name, columnName, type, allowNull, constrain, defaultValue));
            return this.table;
        }
    }
}
