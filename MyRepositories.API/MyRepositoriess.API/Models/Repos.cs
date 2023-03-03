using System.ComponentModel.DataAnnotations;

namespace MyRepositories.API.Models
{
    public class Repos
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public string? RepositorieOwner { get; set; } = "Sem dono declarado";
    }
}
