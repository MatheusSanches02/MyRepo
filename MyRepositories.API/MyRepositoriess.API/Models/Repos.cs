using MyRepositories.API.Interface;

namespace MyRepositoriess.API.Models
{
    public class Repos : IRepos
    {
        public int Id { get; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;
        public DateTime LastUpdate { get; set; }
        public string? RepositorieOwner { get; set; } 
    }
}
