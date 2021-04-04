using SimpleMigrator.DbMigratorEngine.Commands;
using SimpleMigrator.DbMigratorEngine.Commands.Validators;
using System;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public class Table : ITable
    {
        public ISchema Schema { get; }

        public string Name { get; }

        public Table(ISchema schema, string name)
        {
            this.Schema = schema;
            this.Name = name;
        }
    }
}
