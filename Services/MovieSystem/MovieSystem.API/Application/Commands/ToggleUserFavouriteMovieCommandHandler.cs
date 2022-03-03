using MovieSystem.Domain.AggregatesModel.UserAggregate;
using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Application.Commands;
public class ToggleUserFavouriteMovieCommandHandler : IRequestHandler<ToggleUserFavouriteMovieCommand, bool>
{
    private readonly ILogger<ToggleUserFavouriteMovieCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public ToggleUserFavouriteMovieCommandHandler(ILogger<ToggleUserFavouriteMovieCommandHandler> logger, IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<bool> Handle(ToggleUserFavouriteMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user favourite movie details {request}", request);

        Domain.AggregatesModel.UserAggregate.User? user = await _userRepository
            .GetAsync(request.UserId);

        if (user is null)
        {
            throw new ArgumentNullException("User is null for Id - ", nameof(request.UserId));
        }

        user.ToggleFavourite(request.MovieId, request.Reason, request.Rating, request.Liked, request.User);

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}

