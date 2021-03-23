﻿using SimpleCode.Migrator.Cli.TokenRelated;

namespace SimpleCode.Migrator.Cli.Interfaces
{
    public interface ICommand
    {
        public bool CaseSensitive { get; }
        public string ShortOption { get; }
        public string Option { get; }
        public void Execute(MigratorConfiguration migratorConfiguration, TokenManager tokenManager);
    }
}
