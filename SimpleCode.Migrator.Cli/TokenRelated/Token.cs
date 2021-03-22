namespace SimpleMigrator.Migration
{
    public class Token
    {
        public TokenType Type { get; set; }

        public object Value { get; set; }

        public string RawValue { get; set; }
    }
}
