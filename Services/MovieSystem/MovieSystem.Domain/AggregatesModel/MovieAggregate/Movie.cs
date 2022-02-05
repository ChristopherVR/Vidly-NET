using MovieSystem.Domain.Exceptions;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.MovieAggregate;
public class Movie : Entity, IAggregateRoot
{
    public string UpdatedUser { get; private set; }
    public DateTime UpdatedDate { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImdbUrl { get; private set; }

    public Movie(string user, string name, string description, string imdbUrl)
    {
        ValidateDetails(description, name, imdbUrl);
        UpdatedDate = DateTime.Now;
        UpdatedUser = user;
        Name = name;
        Description = description;
        ImdbUrl = imdbUrl;
    }

    public static void ValidateDetails(string description, string name, string imdbUrl)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new MovieDomainException("Description cannot be null");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new MovieDomainException("Name cannot be null");
        }

        if (string.IsNullOrWhiteSpace(imdbUrl))
        {
            throw new MovieDomainException("Imdb cannot be null");
        }

        if (!new System.ComponentModel.DataAnnotations.UrlAttribute().IsValid(imdbUrl))
        {
            throw new MovieDomainException("Imdb url is not a valid URI");
        }
    }

    public void UpdateMovieDetails(string user, string description, string name, string imdbUrl)
    {
        ValidateDetails(description, name, imdbUrl);
        UpdatedDate = DateTime.Now;
        UpdatedUser = user;
        Name = name;
        Description = description;
        ImdbUrl = imdbUrl;
    }

    public static List<Movie> InitialSeedData()
    {
        return new()
        {
            new("Initial", "Star Wars: Episove V - The Empire Strikes Back (1980)", "After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued across the galaxy by Darth Vader and bounty hunter Boba Fett.", "https://www.imdb.com/title/tt0080684/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_15"),
            new("Initial", "The Lord of the Rings: The Return of the King (2003)", "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "https://www.imdb.com/title/tt0167260/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_7"),
            new("Initial", "The Godfather: Part II (1974)", "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", "https://www.imdb.com/title/tt0468569/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_4"),
            new("Initial", "The Godfather (1972)", "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.", "https://www.imdb.com/title/tt0071562/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_3"),
            new("Initial", "Schindler's List (1993)", "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "https://www.imdb.com/title/tt0068646/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_2"),
            new("Initial", "Fight Club (1999)", "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", "https://www.imdb.com/title/tt0108052/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_6"),
        };
    }
}
