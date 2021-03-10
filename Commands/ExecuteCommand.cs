using System.Data.SqlClient;

namespace DornaDbMigrator
{
    public abstract class ExecuteCommand:Command
    {
        public abstract void Execute(SqlConnection connection);
    }
    public abstract class ValidationCommand : Command
    {
        public abstract bool Validate(SqlConnection connection);
    }
    public abstract class Command
    {

    }
}
