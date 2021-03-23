using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Dapper;

namespace SimpleCode.Migrator
{
    public class ExecutionContext
    {
        public SqlConnection SqlConnection { get; set; }
        public IDbTransaction Transaction { get; set; }


        public void Execute(string sql, object param = null)
        {
            var command = new CommandDefinition(sql, param, Transaction);

            var textToShow = sql;
            void ReplaceValues(IDictionary<string, object> dictionary)
            {
                foreach (var item in dictionary)
                {
                    var key = "@" + item.Key;
                    if (textToShow.Contains(key))
                        textToShow = textToShow.Replace(key, item.Value.ToString());
                }
            }

            if (param is ExpandoObject expandoObject)
            {
                ReplaceValues(expandoObject);
            }
            else if (param != null)
            {
                var temp = new Dictionary<string, object>();
                foreach (var item in param.GetType().GetProperties())
                    temp.Add(item.Name, item.GetValue(param));
                ReplaceValues(temp);
            }

            Console.WriteLine(textToShow);

            SqlConnection.Execute(command);
        }
    }
}
