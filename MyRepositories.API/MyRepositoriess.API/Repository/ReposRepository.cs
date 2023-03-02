using Dapper;
using Microsoft.Extensions.Configuration;
using MyRepositories.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace MyRepositories.API.Repository
{
    public class ReposRepository : IReposRepository
    {
        private readonly string _connectionString;

        public ReposRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Repos>> GetAllRepositoriesAsync()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Repos>("SELECT * FROM repos");
        }

        public async Task<IEnumerable<Repos>> GetRepositoriesByNameAsync(string nameFilter)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Repos>("SELECT * FROM repos WHERE name LIKE '%' + @NameFilter + '%'", new { NameFilter = nameFilter });
        }

        public async Task<Repos> GetRepositoryByIdAsync(int id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Repos>("SELECT * FROM repos WHERE id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Repos>> CreateRepositoryAsync(Repos repo)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Repos>("INSERT INTO repos (name, description, language, lastupdate, repositorieowner) VALUES (@Name, @Description, @Language, @LastUpdate, @RepositorieOwner)", repo);
        }

        public async Task<bool> UpdateRepositoryAsync(Repos repo)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync("UPDATE repos SET name = @Name, description = @Description, language = @Language, lastupdate = @LastUpdate, repositorieowner = @RepositorieOwner WHERE id = @Id", repo);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteRepositoryAsync(int id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync("DELETE FROM repos WHERE id = @Id", new { Id = id });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAllRepositoriesAsync()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync("DELETE FROM repos");
            return affectedRows > 0;
        }
        public async Task<bool> FavoriteRepoAsync(Repos repo)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync("INSERT INTO favorite (RepoId) VALUES (@Id)", repo);
            return affectedRows > 0;
        }
        public async Task<IEnumerable<Repos>> GetFavoritesAsync()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Repos>("SELECT R.Name FROM repos R INNER JOIN favorite F ON R.Id = F.RepoId");
        }
    }
}
