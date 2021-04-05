using SimpleMigrator.DbMigratorEngine.Commands;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    //ALTER TABLE Customers ADD Email varchar(255);
    public class ColumnFluent : IColumnName,IColumnType,IColumnOption
    {
        public ISqlCommand SqlCommand { get; }

        public ColumnFluent(ITable table ,ISqlCommand sqlCommand)
        {
            this.SqlCommand = sqlCommand;
        }

        public IColumnType Name(string name)
        {
            SqlCommand.CommandSetions.Add("name",name);
            return this;
        }

        public IColumnOption Default(object value)
        {
            SqlCommand.CommandSetions.Add("defaultValue",$"Default {value}");
            return this;
        }

        public IColumnOption Type(string type)
        {
            SqlCommand.CommandSetions.Add("type",type);
            return this;
        }

        public IColumnOption Constraint(string constraint)
        {
            SqlCommand.CommandSetions.Add("constraint", $"Constraint {constraint}");
            return this;
        }

        public IColumnOption Null()
        {
            SqlCommand.CommandSetions.Add("allowNull", $"Null");
            return this;
        }
        public IColumnOption NotNull()
        {
            SqlCommand.CommandSetions.Add("allowNull", $"Not Null");
            return this;
        }
    }
}
