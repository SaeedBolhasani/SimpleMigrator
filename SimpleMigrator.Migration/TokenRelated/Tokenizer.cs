using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMigrator.Migration
{
    public class Tokenizer
    {
        private static readonly List<Func<string, (TokenType TokenType, object Value)>> tokenRecognizers = 
            new List<Func<string, (TokenType TokenType, object Value)>>
            {
                {i=>(TokenType.Switch,i.StartsWith("--")?i.Substring(2):null )},
                {i=>(TokenType.Switch,i.StartsWith('-')?i.Substring(1):null )},
                {i=>(TokenType.Number,double.TryParse(i,out var output)?output:null )},
                {i=>(TokenType.Boolean,bool.TryParse(i,out var output)?output:null )},
                {i=>(TokenType.String,i)}
            };

        public IEnumerable<Token> Tokenize(string[] rawTokens)
        {
            var tokens = new List<Token>();
            foreach (var item in rawTokens)
            {
                var result = tokenRecognizers.Select(i => i(item)).First(i => i.Value != null);
                var token = new Token
                {
                    Type = result.TokenType,
                    Value = result.Value,
                    RawValue = item
                };
                tokens.Add(token);
            }
            return tokens;
        }
    }
}
