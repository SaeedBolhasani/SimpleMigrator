using SimpleMigrator.DbMigratorEngine.Commands;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    //ALTER TABLE Customers ADD Email varchar(255);
    public class ColumnFluent : IColumnName,IColumnType,IColumnOption
    {
        public ISqlCommand SqlCommand { get; }

        public ColumnFluent(ITable table)
        {
            this.SqlCommand = new AddColumnCommand();
            SqlCommand.CommandSetions.Add("main",$"Alter Table [{table.Schema.Name}].[{table.Name}] Add");
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
            //this.constraint = constraint;
            return this;
        }

        public IColumnOption Null()
        {
            //this.allowNull = true;
            return this;
        }
        public IColumnOption NotNull()
        {
            //this.allowNull = false;
            return this;
        }

        internal AddColumnCommand Build(Table table)
        {

            //return new AddColumnCommand(this.name);
            return null;
        }
    }
}
