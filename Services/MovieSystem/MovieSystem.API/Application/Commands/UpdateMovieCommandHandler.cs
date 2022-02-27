using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using static MovieSystem.API.Application.Commands.MovieCommands;

namespace MovieSystem.API.Application.Commands;
public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, bool>
{
    private readonly ILogger<UpdateMovieCommandHandler> _logger;
    private readonly IGenreRepository _movieRepository;

    public UpdateMovieCommandHandler(ILogger<UpdateMovieCommandHandler> logger, IGenreRepository movieRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating movie {request}", request);

        Movie? movie = await _movieRepository.GetAsync(request.Id);

        if (movie is null)
        {
            throw new ArgumentNullException("Movie is null for Id - ", nameof(request.Id));
        }

        movie
            .UpdateMovieDetails(
            request.User, 
            request.Description, 
            request.Name, 
            request.ImdbUrl);

        _movieRepository.Update(movie);

        await _movieRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}

