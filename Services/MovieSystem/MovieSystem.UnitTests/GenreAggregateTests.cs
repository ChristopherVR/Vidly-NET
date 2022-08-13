using MovieSystem.Domain.AggregatesModel.GenreAggregate;
using Xunit;

namespace MovieSystem.UnitTests.Domain;
public class GenreAggregateTests
{
    [Fact]
    public void Create_genre_success()
    {
        // Arrange 
        string name = "this is a genre";
        // Act
        var user = new Genre(name, "unit_tests");

        // Assert
        Assert.NotNull(user);
    }
}
