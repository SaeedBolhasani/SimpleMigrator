using System;

namespace SimpleMigrator.DbMigratorEngine
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MigrationMetadataAttribute : Attribute
    {
        public MigrationMetadataAttribute(string dornaVersion, string date)
        {
            SimpleMigratorVersion = dornaVersion;
            Date = date;
        }

        public string SimpleMigratorVersion { get; }
        public string Date { get; }
    }
}
