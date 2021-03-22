﻿using SimpleMigrator.DbMigratorEngine.Commands;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMigrator.DbMigratorEngine.Migrators
{
    public abstract class DbMigratorBase
    {
        public List<CommandBase> Commands { get; } = new List<CommandBase>();

        public Table Table(string name)
        {
            return new Table(this, name);
        }

        public void ExecuteRawSql(string sql, object parameters)
        {
            Commands.Add(new RawSqlCommand(sql, parameters));
        }

    }
}