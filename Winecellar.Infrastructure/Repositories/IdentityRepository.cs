using Dapper;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        public IdentityRepository(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<User?> GetByUsernameOrEmail(string loginInput)
        {
            const string sql = @"
                SELECT id, username, email, password_hash 
                FROM users 
                WHERE username = @LoginInput OR email = @LoginInput";

            await using var dbConnection = new Npgsql.NpgsqlConnection(_connectionStrings.DbConnectionString);

            return await dbConnection.QueryFirstOrDefaultAsync<User>(
               sql, new
               {
                   LoginInput = loginInput
               });
        }

        public async Task<Guid> RegisterUser(string email, string username, string password)
        {
            const string sql =
            @"
                INSERT INTO users (id, email, username, password)
                VALUES (@Id, @Email, @Username, @Password)
            ";

            var id = Guid.NewGuid();

            await using var dbConnection = new Npgsql.NpgsqlConnection(_connectionStrings.DbConnectionString);

            await dbConnection.ExecuteScalarAsync(sql, new
            {
                Id = id,
                Email = email,
                Username = username,
                Password = password

            });

            return id;
        }

        public Task StoreRefreshToken(string refreshToken, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}