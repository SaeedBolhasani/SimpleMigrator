namespace DornaDbMigrator.Migrators
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
            this.table.AddCommand(new UpdateCommand("dbo", this.table.Name, this.column, this.value, condition));
            return this.table;
        }
    }
}
