using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using static MovieSystem.API.Application.Commands.MovieCommands;

namespace MovieSystem.API.Application.Commands;
public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
{
    private readonly ILogger<UpdateMovieCommandHandler> _logger;
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieCommandHandler(ILogger<UpdateMovieCommandHandler> logger, IMovieRepository movieRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
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
            request.Title,
            request.NumberInStock,
            request.DailyRentalRate,
            request.GenreId,
            request.ImdbUrl,
            request.Rating);

        _movieRepository.Update(movie);

        await _movieRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return movie;
    }
}
