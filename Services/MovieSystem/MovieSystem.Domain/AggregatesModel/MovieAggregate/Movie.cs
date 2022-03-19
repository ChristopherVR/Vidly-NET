using MovieSystem.Domain.Exceptions;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.MovieAggregate;
public class Movie : Entity, IAggregateRoot
{
    public string UpdatedUser { get; private set; }
    public DateTime UpdatedDate { get; private set; }
    public string Title { get; private set; }
    public string? ImdbUrl { get; private set; }
    public int NumberInStock { get; private set; }
    public int DailyRentalRate { get; private set; }
    public int Rating { get; private set; }
    public int GenreId { get; private set; }

    /// <summary>
    ///  Default EF constructor
    /// </summary>
    /// 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Movie()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }
    private Movie(string user,
        string title,
        int numberInStock,
        int dailyRentalRate,
        string imdbUrl,
        int rating)
    {
        UpdatedDate = DateTime.MinValue;
        UpdatedUser = user;
        Title = title;
        NumberInStock = numberInStock;
        GenreId = 1;
        DailyRentalRate = dailyRentalRate;
        ImdbUrl = imdbUrl;
        Rating = rating;
    }

    public Movie(
        string user,
        string title,
        int numberInStock,
        int dailyRentalRate,
        int genreId, 
        string imdbUrl,
        int rating)
    {
        ValidateDetails(title, numberInStock, dailyRentalRate, genreId, imdbUrl);
        UpdatedDate = DateTime.Now;
        UpdatedUser = user;
        Title = title;
        NumberInStock = numberInStock;
        GenreId = genreId;
        DailyRentalRate = dailyRentalRate;
        ImdbUrl = imdbUrl;
        Rating = rating;
    }

    public static void ValidateDetails(
        string title,
        int numberInStock,
        int dailyRentalRate,
        int genreId,
        string imdbUrl)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new MovieDomainException("Title cannot be null");
        }

        if (numberInStock == default)
        {
            throw new MovieDomainException("NumberInStock cannot be default");
        }

        if (dailyRentalRate == default)
        {
            throw new MovieDomainException("DailyRentalRate cannot be default");
        }

        if (genreId == default)
        {
            throw new MovieDomainException("GenreId cannot be default");
        }

        if (!string.IsNullOrWhiteSpace(imdbUrl) && !new System.ComponentModel.DataAnnotations.UrlAttribute().IsValid(imdbUrl))
        {
            throw new MovieDomainException("Imdb url is not a valid URI");
        }
    }

    public void UpdateMovieDetails(
        string user,
        string title,
        int numberInStock,
        int dailyRentalRate,
        int genreId,
        string imdbUrl,
        int rating)
    {
        ValidateDetails(title, numberInStock, dailyRentalRate, genreId, imdbUrl);
        UpdatedDate = DateTime.Now;
        UpdatedUser = user;
        Title = title;
        NumberInStock = numberInStock;
        GenreId = genreId;
        DailyRentalRate = dailyRentalRate; ;
        ImdbUrl = imdbUrl;
        Rating = rating;
    }

    public static List<Movie> InitialSeedData()
    {
        return new()
        {
            new("Initial", "Star Wars: Episove V - The Empire Strikes Back (1980)", 20, 5, "https://www.imdb.com/title/tt0080684/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_15", 5)
            {
                Id = 1,
            },
            new("Initial", "The Lord of the Rings: The Return of the King (2003)", 20, 5, "https://www.imdb.com/title/tt0167260/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_7", 5)
            {
                Id = 2,
            },
            new("Initial", "The Godfather: Part II (1974)", 20, 5, "https://www.imdb.com/title/tt0468569/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_4", 5)
            {
                Id = 3,
            },
            new("Initial", "The Godfather (1972)", 20, 5, "https://www.imdb.com/title/tt0071562/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_3", 5)
            {
                Id = 4,
            },
            new("Initial", "Schindler's List (1993)", 20, 5, "https://www.imdb.com/title/tt0068646/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_2", 5)
            {
                Id = 5,
            },
            new("Initial", "Fight Club (1999)", 20, 5, "https://www.imdb.com/title/tt0108052/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_6", 5)
            {
                Id = 6,
            },
        };
    }
}
