using MovieSystem.Domain.AggregatesModel.UserAggregate;
using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Application.Commands;
public class CreateUpdateUserHashedPasswordCommandHandler : IRequestHandler<CreateUpdateUserPasswordCommand, bool>
{
    private readonly ILogger<CreateUpdateUserHashedPasswordCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CreateUpdateUserHashedPasswordCommandHandler(ILogger<CreateUpdateUserHashedPasswordCommandHandler> logger, IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<bool> Handle(CreateUpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user {request}", request);

        User? user = await _userRepository
            .GetAsync(request.Id);

        if (user is null)
        {
            throw new ArgumentNullException("User is null for Id - ", nameof(request.Id));
        }

        user.UpdatePassword(request.User, request.HashedPassword);

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}

