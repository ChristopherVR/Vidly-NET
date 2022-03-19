using MovieSystem.Domain.AggregatesModel.UserAggregate;
using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Application.Commands;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user {request}", request);

        User? user = await _userRepository
            .GetAsync(request.UserName);

        if (user is not null)
        {
            throw new ArgumentException(nameof(request.UserName), $"User already exists with Username - ${request.UserName}");
        }

        user = new User(request.Name, request.UserName, request.Surname, string.Empty, request.PhoneNumber, request.Address, request.HomeNumber, request.ImageUrl);

        _userRepository.Add(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return user;
    }
}

