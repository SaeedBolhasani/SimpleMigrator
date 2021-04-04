using SimpleMigrator.DbMigratorEngine.Migrators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCode.Migrator.Migrators.Extensions
{
    public static class AlterExtensions
    {
        public static IAddColumn AddColumn(this IAlter alter,Func<IColumnName, IColumnOption> column)
        {
            var columnFluent = new ColumnFluent(alter.Table);
            column(columnFluent);

            alter.Table.Schema.DbMigratorBase.Commands.Add(columnFluent.SqlCommand);
            return alter as IAddColumn;
        }
    }
}
