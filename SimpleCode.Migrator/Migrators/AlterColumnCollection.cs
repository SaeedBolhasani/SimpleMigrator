using System.Data;
using SimpleCode.Migrator.Models;

namespace SimpleCode.Migrator.Migrators
{
    public class AlterColumnCollection : Alter
    {
        public AlterColumnCollection(Table table) : base(table)
        {
        }

        public new AlterColumnCollection AddColumnIfNoExist(string columnName, SqlDbType type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity=null)
        {
            base.AddColumnIfNoExist(columnName, type, allowNull, defaultValue, constraint,identity);
            return this;
        }

        public new AlterColumnCollection AddColumnIfNoExist(string columnName, string type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
        {
            base.AddColumnIfNoExist(columnName, type, allowNull, defaultValue, constraint);
            return this;
        }

        public new AlterColumnCollection DropColumn(string columnName)
        {
            base.DropColumn(columnName);
            return this;
        }

        public new AlterColumnCollection DropColumnIfNotExist(string columnName)
        {
            base.DropColumnIfNotExist(columnName);
            return this;
        }

        public new AlterColumnCollection DropConstraint(string constraint)
        {
            base.DropConstraint(constraint);
            return this;
        }

        public new AlterColumnCollection DropConstraintIfNotExists(string constraint)
        {
            base.DropConstraintIfNotExists(constraint);
            return this;
        }

        public AlterColumnCollection ModifyColumn(string columnName, SqlDbType type, bool allowNull = true, object defaultValue = null, string constraint = null, Identity identity = null)
    }
}
