using System.Collections.Generic;
using System.Threading.Tasks;
using MyRepositories.API.Models;

namespace MyRepositories.API.Repository
{
    public interface IReposRepository
    {
        Task<IEnumerable<Repos>> GetAllRepositoriesAsync();
        Task<IEnumerable<Repos>> GetRepositoriesByNameAsync(string nameFilter);
        Task<Repos> GetRepositoryByIdAsync(int id);
        Task<IEnumerable<Repos>> CreateRepositoryAsync(Repos repository);
        Task<bool> UpdateRepositoryAsync(Repos repository);
        Task<bool> DeleteRepositoryAsync(int id);
        Task<bool> DeleteAllRepositoriesAsync();
    }
}
