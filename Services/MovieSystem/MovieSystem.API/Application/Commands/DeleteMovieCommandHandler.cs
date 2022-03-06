using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using static MovieSystem.API.Application.Commands.MovieCommands;

namespace MovieSystem.API.Application.Commands;
public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, bool>
{
    private readonly ILogger<DeleteMovieCommandHandler> _logger;
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieCommandHandler(ILogger<DeleteMovieCommandHandler> logger, IMovieRepository movieRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new movie {request}", request);

        Movie? movie = await _movieRepository.GetAsync(request.Id);

        if (movie is null)
        {
            throw new ArgumentNullException("Movie is null for Id - ", nameof(request.Id));
        }
        
        _movieRepository.Remove(movie);

        await _movieRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}

