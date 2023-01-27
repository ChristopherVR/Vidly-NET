using System.Security.Claims;

namespace MovieSystem.API.Infrastructure.Authorization;
public class UserHasSameIdAccessAuthorizationHandler
    : AuthorizationHandler<UserAccessRequirement>
{
    private readonly ILogger<UserHasSameIdAccessAuthorizationHandler> _logger;

    public UserHasSameIdAccessAuthorizationHandler(ILogger<UserHasSameIdAccessAuthorizationHandler> logger) => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessRequirement requirement)
    {
        if (int.TryParse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
        {
            if (userId == requirement.UserId)
            {
                context.Succeed(requirement);
            }
        }

        if (!context.HasSucceeded)
        {
            _logger.LogWarning("User Id does not match");
        }
        return Task.CompletedTask;
    }
}

public class UserAccessRequirement : IAuthorizationRequirement
{
    public int UserId { get; }

    public UserAccessRequirement(int userId)
    => UserId = userId;
}
