using MovieSystem.Domain.AggregatesModel.UserAggregate;
using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Application.Commands;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user {request}", request);

        Domain.AggregatesModel.UserAggregate.User? user = await _userRepository
            .GetAsync(request.Id);

        if (user is null)
        {
            throw new ArgumentNullException("User is null for Id - ", nameof(request.Id));
        }

        user.Update(request.User, request.Name, request.Surname, request.PhoneNumber, request.Address, request.HomeNumber, request.ImageUrl);

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return user;
    }
}

