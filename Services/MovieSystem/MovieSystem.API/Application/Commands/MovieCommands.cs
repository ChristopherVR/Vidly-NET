using MovieSystem.Domain.AggregatesModel.MovieAggregate;

namespace MovieSystem.API.Application.Commands;
public class MovieCommands
{
    public record CreateMovieCommand(
        string Title,
        int NumberInStock,
        int Rating,
        int GenreId,
        int DailyRentalRate,
        string User,
        string ImdbUrl) : IRequest<Movie>;

    public record UpdateMovieCommand(
        int Id,
        string Title,
        int NumberInStock,
        int Rating,
        int GenreId,
        int DailyRentalRate,
        string User,
        string ImdbUrl) : IRequest<Movie>;

    public record DeleteMovieCommand(int Id) : IRequest<bool>;
}
