using System.Collections.Generic;
using System.Linq;

namespace DornaDbMigrator
{
    public abstract class DbMigratorBase
    {
        public List<Command> Commands { get; } = new List<Command>();

        public Table Table(string name)
        {
            return new Table(this, name);
        }

    }
}
