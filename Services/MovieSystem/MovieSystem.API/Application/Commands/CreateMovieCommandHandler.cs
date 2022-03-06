using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using static MovieSystem.API.Application.Commands.MovieCommands;

namespace MovieSystem.API.Application.Commands;
public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
{
    private readonly ILogger<CreateMovieCommandHandler> _logger;
    private readonly IMovieRepository _movieRepository;

    public CreateMovieCommandHandler(ILogger<CreateMovieCommandHandler> logger, IMovieRepository movieRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new movie {request}", request);

        Movie movie = new(
            request.User,
            request.Title,
            request.NumberInStock,
            request.DailyRentalRate,
            request.GenreId,
            request.ImdbUrl,
            request.Rating);

        _movieRepository.Add(movie);

        await _movieRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return movie;
    }
}

