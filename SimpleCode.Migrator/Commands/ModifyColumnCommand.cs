namespace SimpleCode.Migrator.Commands
{
    public class ModifyColumnCommand : CommandBase
    {
        private readonly string table;
        private readonly string column;
        private readonly string type;
        private readonly bool allowNull;

        //ToDo: validations??

        public ModifyColumnCommand(string table, string column, string type, bool allowNull)
        {
            this.table = table;
            this.column = column;
            this.type = type;
            this.allowNull = allowNull;
        }

        public override void Execute(ExecutionContext executionContext)
        {
            var nullability = allowNull ? "Null " : "Not Null ";
            var sql = $"ALTER TABLE {table} MODIFY {column} {type} {nullability};";

            executionContext.Execute(sql);
        }
    }
}