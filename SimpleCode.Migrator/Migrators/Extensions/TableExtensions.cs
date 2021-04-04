using SimpleMigrator.DbMigratorEngine.Migrators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCode.Migrator.Migrators.Extensions
{
    public static class TableExtensions
    {
        public static IAlter Alter(this ITable table)
        {
            return new Alter(table);
        }
    }
}
