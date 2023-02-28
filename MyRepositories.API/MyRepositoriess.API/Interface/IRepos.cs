namespace MyRepositories.API.Interface
{
    public interface IRepos
    {
        int Id { get; }
        string Name { get; set; }
        string Description { get; set; }
        string Language { get; set; }
        DateTime LastUpdate { get; set; }
        string? RepositorieOwner { get; set; }
    }
}
