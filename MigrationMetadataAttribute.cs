using System;

namespace DornaDbMigrator
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MigrationMetadataAttribute : Attribute
    {
        public MigrationMetadataAttribute(string dornaVersion, string date)
        {
            DornaVersion = dornaVersion;
            Date = date;
        }

        public string DornaVersion { get; }
        public string Date { get; }
    }
}
