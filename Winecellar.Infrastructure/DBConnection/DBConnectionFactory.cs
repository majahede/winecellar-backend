using System.Data;

namespace Winecellar.Infrastructure.DBConnection
{
    public static class DbConnectionFactory
    {
        public static Func<IDbConnection> CreateFactory(string connectionString)
        {
            return () => new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
