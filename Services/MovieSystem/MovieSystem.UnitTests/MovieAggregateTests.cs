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
        string description = "this is my description";
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        // Act
        Movie movie = new (user, name, description, imdbUrl);

        // Assert
        Assert.NotNull(movie);
    }

    [Fact]
    public void Create_movie_imdb_url_null_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        string description = "this is my description";
        string imdbUrl = " ";
        string user = "unit_tests";
        var expectedErrorMessage = "Imdb cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, description, imdbUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_imdb_url_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        string description = "this is my description";
        string imdbUrl = "htasdasdtps://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "Imdb url is not a valid URI";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, description, imdbUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_description_null_invalid_exception()
    {
        // Arrange 
        string name = "this is a name";
        string description = "";
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "Description cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, description, imdbUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_movie_name_null_invalid_exception()
    {
        // Arrange 
        string name = "";
        string description = "this is my description";
        string imdbUrl = "https://www.imdb.com/title/tt3230854/episodes?season=6";
        string user = "unit_tests";
        var expectedErrorMessage = "Name cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new Movie(user, name, description, imdbUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
}

