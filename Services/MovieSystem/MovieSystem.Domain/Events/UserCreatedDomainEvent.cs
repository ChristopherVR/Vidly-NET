using MovieSystem.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace MovieSystem.Domain.Events;

public class UserCreatedDomainEvent : INotification
{
    public User User { get; private set; }

    public UserCreatedDomainEvent(User user)
    {
        User = user;
    }
}

