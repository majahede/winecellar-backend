using Dapper;
using System.Data;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly Func<IDbConnection> _dbConnectionFactory;

        public IdentityRepository(Func<IDbConnection> dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<User?> GetByUsernameOrEmail(string loginInput)
        {
            const string sql = @"
                SELECT id, username, email, password
                FROM users 
                WHERE username = @LoginInput OR email = @LoginInput";

            using var dbConnection = _dbConnectionFactory.Invoke();

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

            using var dbConnection = _dbConnectionFactory.Invoke();

            await dbConnection.ExecuteAsync(sql, new
            {
                Id = id,
                Email = email,
                Username = username,
                Password = password

            });

            return id;
        }

        public async Task StoreRefreshToken(string token, Guid userId)
        {
            const string sql =
            @"
                INSERT INTO refresh_tokens (id, user_id, token)
                VALUES (@Id, @UserId, @Token)
            ";

            var id = Guid.NewGuid();

            using var dbConnection = _dbConnectionFactory.Invoke();

            await dbConnection.ExecuteAsync(
                sql, new
                {
                    Id = id,
                    Token = token,
                    UserId = userId
                });
        }
    }
}