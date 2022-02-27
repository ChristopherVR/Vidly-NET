using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using static MovieSystem.API.Application.Commands.MovieCommands;

namespace MovieSystem.API.Application.Commands;
public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly ILogger<CreateMovieCommandHandler> _logger;
    private readonly IGenreRepository _movieRepository;

    public CreateMovieCommandHandler(ILogger<CreateMovieCommandHandler> logger, IGenreRepository movieRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new movie {request}", request);

        Movie movie = new(
            request.User,
            request.Name,
            request.Description,
            request.ImdbUrl);

        _movieRepository.Add(movie);

        await _movieRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return movie.Id;
    }
}

