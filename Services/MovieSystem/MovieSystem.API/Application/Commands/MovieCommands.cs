namespace MovieSystem.API.Application.Commands;
public class MovieCommands
{
    public record CreateMovieCommand(
        string Name,
        string Description,
        string User,
        string ImdbUrl) : IRequest<int>;

    public record UpdateMovieCommand(
        int Id,
        string Name,
        string Description,
        string User,
        string ImdbUrl) : IRequest<bool>;

    public record DeleteMovieCommand(int Id) : IRequest<bool>;
}