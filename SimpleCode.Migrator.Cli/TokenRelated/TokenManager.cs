using System.Collections.Generic;

namespace SimpleCode.Migrator.Cli.TokenRelated
{
    public class TokenManager
    {
        private readonly Queue<Token> tokens;
        public TokenManager(IEnumerable<Token> tokens)
        {
            this.tokens = new Queue<Token>(tokens);
        }

        public Token GetToken()
        {
            return tokens.Peek();
        }

        public void Consume()
        {
            tokens.Dequeue();
        }

        public bool HasToken()
        {
            return tokens.Count != 0;
        }
    }
}
