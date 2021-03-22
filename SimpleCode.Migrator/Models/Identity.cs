namespace SimpleMigrator.DbMigratorEngine.Models
{
    public class Identity
    {
        public Identity(int seed, int increament)
        {
            Seed = seed;
            Increament = increament;
        }
        public int Seed { get; }
        public int Increament { get; }

    }
}
