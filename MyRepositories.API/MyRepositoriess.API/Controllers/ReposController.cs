using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyRepositoriess.API.Models;
using System.Data.SqlClient;

namespace MyRepositoriess.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ReposController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Repos>>> GetAllRepositories()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var repos = await connection.QueryAsync<Repos>("select * from repos");
            return Ok(repos);
        }
    }
}
