using System.ComponentModel.DataAnnotations.Schema;

namespace MyRepositoriess.API.Models
{
    public class Repos
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;
        public DateTime LastUpdate { get; set; }
        public string? RepositorieOwner { get; set; } 
    }
}
