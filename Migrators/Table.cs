using DornaDbMigrator.Migrators;

namespace DornaDbMigrator
{
    public class Table
    {
        private readonly DbMigratorBase dbMigrator;
        public string Name { get; }

        public Table(DbMigratorBase dbMigrator, string name)
        {
            this.dbMigrator = dbMigrator;
            this.Name = name;
        }
        public Alter Alter()
        {
            return new Alter(this);
        }

        public Insert Insert(params string[] columns)
        {
            return new Insert(this, columns);
        }

        public Insert InsertIfNotExist(params string[] columns)
        {
            return new Insert(this, columns,true);
        }

        public Update Update(string column,object value)
        {
            return new Update(this, column, value);
        }

        public void AddCommand(Command command)
        {
            this.dbMigrator.Commands.Add(command);
        }
    }
}
