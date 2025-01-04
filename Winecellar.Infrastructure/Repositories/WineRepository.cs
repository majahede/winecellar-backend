using Dapper;
using System.Data;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Wines;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Repositories
{
    public class WineRepository : IWineRepository
    {
        private readonly Func<IDbConnection> _dbConnectionFactory;

        public WineRepository(Func<IDbConnection> dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Wine>> GetAllWines()
        {
            var sql = "SELECT id, name FROM wines";

            using var dbConnection = _dbConnectionFactory.Invoke();
            
            return await dbConnection.QueryAsync<Wine>(sql);
        }

        public async Task<Guid> CreateWine(CreateWineRequestDto request)
        {
            const string sql =
            @"
                INSERT INTO wines (id, name)
                VALUES (@Id, @Name)
            ";

            var id = Guid.NewGuid();

            using var dbConnection = _dbConnectionFactory.Invoke();

            await dbConnection.ExecuteAsync(sql, new
            {
                Id = id,
                request.Name
            });

            return id;

        }
    }
}