using Dapper;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Wines;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Repositories
{
    public class WineRepository : IWineRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public WineRepository(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IEnumerable<Wine>> GetAllWines()
        {
            var sql = "SELECT id, name FROM wines";

            await using var dbConnection = new Npgsql.NpgsqlConnection(_connectionStrings.DbConnectionString);
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

            await using var dbConnection = new Npgsql.NpgsqlConnection(_connectionStrings.DbConnectionString);

            await dbConnection.ExecuteScalarAsync(sql, new
            {
                Id = id,
                request.Name
            });

            return id;

        }
    }
}
