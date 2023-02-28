using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyRepositoriess.API.Models;
using System.Data.SqlClient;

namespace MyRepositoriess.API.Controllers
{
    //Define a rota padrão da api.
    [Route("api/[controller]")]
    [ApiController]

    //controller herda da classe abstrata ControllerBase
    public class ReposController : ControllerBase
    {
        //readonly funciona como constante e é definido na inicialização do construtor.
        private readonly IConfiguration _configuration;

        //construtor para definição de conexão com banco de dados.
        public ReposController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        //Task<ActionResult<T>> permite a execução de métodos assíncronos melhorando o desempenho da aplicação e encapsula o reultado em um objeto ActionResult<T> que fornece informações sobre o resultado da ação.
        public async Task<ActionResult<List<Repos>>> GetAllRepositories()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //A interface IEnumerable<T> é utilizada para representar coleções de objetos que podem ser percorridas sequencialmente, no nosso caso, vamos percorrer sobre uma lista de repositórios
            IEnumerable<Repos> repos = await SelectRepositories(connection);
            //retorna 200 e os repositórios obtidos do banco
            return Ok(repos);
        }

        [HttpGet("name/{nameFilter}")]
        public async Task<ActionResult<Repos>> GetRepositorieByName(string nameFilter)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var name = await connection.QueryAsync<Repos>("select * from repos where name like @Name", new { Name = "%" + nameFilter + "%" });
            return Ok(name);
        }

        [HttpGet("{repoId}")]
        public async Task<ActionResult<Repos>> GetRepositorieById(int repoId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var repo = await connection.QueryFirstAsync<Repos>("select * from repos where id = @Id", new { Id = repoId });
            return Ok(repo);
        }

        [HttpPost]
        public async Task<ActionResult<List<Repos>>> CreateRepositorie(Repos repo)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into repos (name, description, language, lastupdate, repositorieowner) values (@Name, @Description, @Language, @LastUpdate, @RepositorieOwner)", repo);
            return Ok(await SelectRepositories(connection));
        }

        [HttpPut]
        public async Task<ActionResult<List<Repos>>> UpdateRepositorie(Repos repo)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("update repos set name = @Name, description = @Description, language = @Language, lastupdate = @LastUpdate, repositorieowner = @RepositorieOwner where id = @id", repo);
            return Ok(await SelectRepositories(connection));
        }

        [HttpDelete("{repoId}")]
        public async Task<ActionResult<List<Repos>>> DeleteRepositorie(int repoId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from repos where id = @Id", new { Id = repoId });
            return Ok(await SelectRepositories(connection));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Repos>>> DeleteAllRepositories()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from repos");
            return Ok(await SelectRepositories(connection));
        }

        //extração para reutilizarmos o método, que a princípio foi criado no get de todos os repositórios
        private static async Task<IEnumerable<Repos>> SelectRepositories(SqlConnection connection)
        {
            return await connection.QueryAsync<Repos>("select * from repos");
        }
    }
}
