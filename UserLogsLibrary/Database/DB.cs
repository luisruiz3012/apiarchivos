using Microsoft.Data.SqlClient;

namespace UserLogsLibrary.Database
{
    public class DB
    {
        private readonly string _connectionString;

        public DB()
        {
            _connectionString = @"Data source = localhost;Initial Catalog = master;User ID = sa;Password = Password12345;TrustServerCertificate = True ";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
