using SimpleCode.Migrator.Commands;

namespace SimpleCode.Migrator.Migrators
{
    public class Update
    {
        private readonly Table table;
        private readonly string column;
        private readonly object value;

        public Update(Table table, string column, object value)
        {
            this.table = table;
            this.column = column;
            this.value = value;
        }


        public Table Where(string condition)
        {
            table.AddCommand(new UpdateCommand("dbo", table.Name, column, value, condition));
            return table;
        }
    }
}
