using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using MovieSystem.Domain.Exceptions;
using System;
using Xunit;

namespace MovieSystem.UnitTests.Domain;
public class MovieAggregateTests
{
    [Fact]
    public void Create_movie_success()
    {
        // Arrange 
        string name = "this is a name";
        int numberInStock = 1;
        int dailyRentalRate = 2;
        int rating = 4;
        int genreId = 1;
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        // Act
        Movie movie = new (user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public void Create_movie_imdb_url_null_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        int numberInStock = 1;
        int dailyRentalRate = 2;
        int rating = 4;
        int genreId = 1;
        string imdbUrl = " ";
        string user = "unit_tests";
        var expectedErrorMessage = "Imdb url is not a valid URI";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_imdb_url_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        int numberInStock = 1;
        int dailyRentalRate = 2;
        int rating = 4;
        int genreId = 1;
        string imdbUrl = "htasdasdtps://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "Imdb url is not a valid URI";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_genre_default_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        int numberInStock = 1;
        int dailyRentalRate = 2;
        int rating = 4;
        int genreId = default;
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "GenreId cannot be default";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_number_in_stock_default_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        int numberInStock = default;
        int dailyRentalRate = 2;
        int rating = 4;
        int genreId = 1;
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "NumberInStock cannot be default";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_daily_rental_default_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        int numberInStock = 1;
        int dailyRentalRate = default;
        int rating = 4;
        int genreId = 2;
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "DailyRentalRate cannot be default";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_name_null_invalid_exception()
    {
        // Arrange 
        string name = "";
        int numberInStock = 1;
        int dailyRentalRate = 2;
        int rating = 4;
        int genreId = 1;
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "Title cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, numberInStock, dailyRentalRate, genreId, imdbUrl, rating));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
}

