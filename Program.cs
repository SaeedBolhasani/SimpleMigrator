using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DornaDbMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
                Console.WriteLine("[dorna version] [connection string]");

            var version = args[0];
            var conncetionString = args[1];

            string sqlOrderDetails = "SELECT TOP 5 * FROM OrderDetails;";
            string sqlOrderDetail = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID;";
            string sqlCustomerInsert = "INSERT INTO Customers (CustomerName) Values (@CustomerName);";
            var a = @"SELECT COLUMN_NAME

FROM INFORMATION_SCHEMA.COLUMNS

WHERE TABLE_NAME = 'Sec_User'

ORDER BY ORDINAL_POSITION";

            using (var connection = new SqlConnection(conncetionString))
            {
                //var affectedRows = connection.Execute(sqlCustomerInsert, new { CustomerName = "Mark" });
                // var affectedRows = connection.Query(a);
                foreach (var item in new FirstMigration().Commands)
                {
                    if(item is ValidationCommand validationCommand)
                    {
                        var www = validationCommand.Validate(connection);
                    }
                    else if(item is ExecuteCommand executeCommand)
                    {
                        executeCommand.Execute(connection);
                    }
                }
                //Console.WriteLine(affectedRows);
               
            }

            Console.ReadKey();
        }
    }
}
