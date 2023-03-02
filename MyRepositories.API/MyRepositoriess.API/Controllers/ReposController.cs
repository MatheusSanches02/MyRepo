using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyRepositories.API.Models;
using MyRepositories.API.Repository;
using System.Data.SqlClient;

namespace MyRepositories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private readonly IReposRepository _reposRepository;

        public ReposController(IReposRepository reposRepository)
        {
            _reposRepository = reposRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRepositoriesAsync()
        {
            try
            {
                var repos = await SelectRepositoriesAsync(_reposRepository);
                return Ok(repos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("findName/{repoName}")]
        public async Task<IActionResult> GetRepositorieByNameAsync(string repoName)
        {
            try
            {
                var name = await _reposRepository.GetRepositoriesByNameAsync(repoName);
                if(name == null)
                {
                    return NotFound();
                }
                return Ok(name);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("findId/{repoId}")]
        public async Task<IActionResult> GetRepositorieByIdAsync(int repoId)
        {
            try
            {
                var id = await _reposRepository.GetRepositoryByIdAsync(repoId);
                if (id == null)
                {
                    return NotFound();
                }
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRepositorieAsync(Repos repo)
        {
            try
            {
                var repos = await _reposRepository.CreateRepositoryAsync(repo);
                return Ok(await SelectRepositoriesAsync(_reposRepository));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRepositoryAsync(int id, Repos repo)
        {
            try
            {
                var repoId = await _reposRepository.GetRepositoryByIdAsync(id);
                if (repoId == null)
                    return NotFound();

                repo.Id = id;

                var repos = await _reposRepository.UpdateRepositoryAsync(repo);
                return Ok(repos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("deleteId/{repoId}")]
        public async Task<IActionResult> DeleteRepositoryAsync(int repoId)
        {
            try
            {
                var repos = await _reposRepository.DeleteRepositoryAsync(repoId);
                return Ok(repos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAllRepositoriesAsync()
        {
            try
            {
                var repos = await _reposRepository.DeleteAllRepositoriesAsync();
                return Ok(repos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("favorite")]
        public async Task<IActionResult> GetFavoriteAsync()
        {
            try
            {
                var favorite = await _reposRepository.GetFavoritesAsync();
                return Ok(favorite);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("addFavorite")]
        public async Task<IActionResult> FavoriteRepoAsync(Repos repo)
        {
            try
            {
                var favorite = await _reposRepository.FavoriteRepoAsync(repo);
                return Ok(favorite);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private static async Task<IEnumerable<Repos>> SelectRepositoriesAsync(IReposRepository _reposRepository)
        {
            return await _reposRepository.GetAllRepositoriesAsync();
        }
    }
}
