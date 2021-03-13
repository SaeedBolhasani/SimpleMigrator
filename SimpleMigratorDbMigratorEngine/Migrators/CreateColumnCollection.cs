using SimpleMigrator.DbMigratorEngine.Models;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public class CreateColumnCollection
    {
        public List<string> Columns { get; } = new List<string>();

        public CreateColumnCollection()
        {

        }
        public CreateColumnCollection AddColumn(string columnName, SqlDbType type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            this.Columns.Add(RowSql(columnName, type.ToString(), allowNull, defaultValue, constraint, identity));
            return this;
        }
        public CreateColumnCollection AddColumn(string columnName, string type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            this.Columns.Add(RowSql(columnName, type, allowNull, defaultValue, constraint, identity));
            return this;
        }

        private string RowSql(string columnName, string type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("[{0}] {1}  ",
                 columnName, type);

            stringBuilder.Append(allowNull ? "Null " : "Not Null ");

            if (constraint != null)
                stringBuilder.AppendFormat("CONSTRAINT [{0}] ", constraint);

            if (defaultValue != null)
                stringBuilder.AppendFormat("DEFAULT (({0})) ", defaultValue);

            if (identity != null)
                stringBuilder.AppendFormat($"IDENTITY({identity.Seed},{identity.Increament}) ");

            return stringBuilder.ToString();
        }

    }
}
