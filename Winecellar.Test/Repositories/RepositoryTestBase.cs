using Dapper;
using Npgsql;
using System.Data;

namespace Winecellar.Test.Repositories
{
    public abstract class RepositoryTestBase : IAsyncLifetime
    {
        protected Func<IDbConnection> DbConnectionFactory;

        public RepositoryTestBase()
        {
            DbConnectionFactory = CreateTestDatabase();  
        }

        public async Task InitializeAsync()
        {
            await CreateTables();
        }

        public async Task DisposeAsync()
        {
            await Cleanup();
        }

        protected Func<IDbConnection> CreateTestDatabase()
        {
            var connectionString = "Host=localhost;Port=5432;Database=winecellartestdb;Username=postgres;Password=password";
            return () => new NpgsqlConnection(connectionString);
        }
        public async Task SeedUser(Guid userId, string username, string email, string password)
        {
            
            using var connection = DbConnectionFactory.Invoke();

            await connection.ExecuteAsync(@"
                INSERT INTO users (id, username, email, password)
                VALUES (@Id, @Username, @Email, @Password)",
                new
                {
                    Id = userId,
                    Username = username,
                    Email = email,
                    Password = password
                });
        }

        public async Task CreateTables()
        {
            const string sql = @"
            CREATE TABLE IF NOT EXISTS users (
                id UUID PRIMARY KEY,
                username VARCHAR(50) NOT NULL,
                email VARCHAR(100) NOT NULL,
                password VARCHAR(60) NOT NULL
            );
        ";

            using var connection = DbConnectionFactory.Invoke();
            await connection.ExecuteAsync(sql);
        }

        public async Task Cleanup()
        {
            using var connection = DbConnectionFactory.Invoke();
            await connection.ExecuteAsync("DELETE FROM users;");
        }
    }
}
