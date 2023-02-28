﻿using MyRepositories.API.Interface;
using System.ComponentModel.DataAnnotations;

namespace MyRepositoriess.API.Models
{
    public class Repos : IRepos
    {
        public int Id { get; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public string? RepositorieOwner { get; set; } 
    }
}
