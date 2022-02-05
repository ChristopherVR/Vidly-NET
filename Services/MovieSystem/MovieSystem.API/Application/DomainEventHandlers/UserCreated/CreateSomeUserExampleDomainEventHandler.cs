using MediatR;
using MovieSystem.Domain.Events;

namespace MovieSystem.API.Application.DomainEventHandlers.UserCreated;

public class CreateSomeUserExampleDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly ILogger<CreateSomeUserExampleDomainEventHandler> _logger;
    public CreateSomeUserExampleDomainEventHandler(ILogger<CreateSomeUserExampleDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UserCreatedDomainEvent userCreatedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("The user created domain event handler triggered. {user}", userCreatedDomainEvent.User);
        await Task.CompletedTask;
    }
}

