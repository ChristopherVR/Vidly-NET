using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.GenreAggregate;
public class Genre : Entity, IAggregateRoot
{
    public string UpdatedUser { get; private set; }
    public DateTime UpdatedDate { get; private set; } = DateTime.Now;
    public string Name { get; private set; }

    private Genre(int id, string name)
    {
        Id = id;
        UpdatedUser = "initial";
        UpdatedDate = DateTime.MinValue;
        Name = name;
    }

    public Genre(string name, string username)
    {
        Name = name;
        UpdatedUser = username;
    }

    public static List<Genre> CreateInitialSeedData() => new ();

}
