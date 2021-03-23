namespace SimpleCode.Migrator.Models
{
    public class Identity
    {
        public Identity(int seed, int increment)
        {
            Seed = seed;
            Increment = increment;
        }
        public int Seed { get; }
        public int Increment { get; }

    }
}
