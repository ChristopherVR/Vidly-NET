using Google.Protobuf.WellKnownTypes;
using MovieSystem.API.Application.Queries;
using MovieSystem.API.Infrastructure.Authorization;
using System.Security.Claims;
using UserSystem.V1;
using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Grpc;

[Authorize]
public class UsersServiceV1 : Users.UsersBase
{
    private readonly ILogger<UsersServiceV1> _logger;
    private readonly IMediator _mediator;
    private readonly IUserQueries _userQueries;
    private readonly IAuthorizationService _authorizationService;

    public UsersServiceV1(
        ILogger<UsersServiceV1> logger,
        IMediator mediator, 
        IUserQueries userQueries,
        IAuthorizationService authService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
        _authorizationService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public override async Task<UserExtended> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Updating user information for User - {id}", request.Id);
        var command = new UpdateUserCommand(request.Id, request.Name, request.Address, request.ImageUrl, request.PhoneNumber, request.HomeNumber, request.Surname, "editedUser");

        var result = await _mediator.Send(command, context.CancellationToken);

        return new()
        {
            Address = request.Address,
            HomeNumber = request.HomeNumber,
            Id = request.Id,
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Surname = request.Surname,
            Username = result.Username,
            HashedPassword = result.HashedPassword,
        };
    }

    public override async Task<Empty> ToggleUserFavouriteMovie(ToggleUserFavouriteMovieRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Toggling Movie Id - {movieId} for User - {userId}", request.MovieId, request.UserId);
        string username = context.GetHttpContext().User.FindFirst(ClaimTypes.Name)?.Value ?? throw new ArgumentException("Username cannot be null");

        var command = new ToggleUserFavouriteMovieCommand(
            request.UserId,
            request.MovieId,
            username,
            request.Liked);

        bool result = await _mediator.Send(command, context.CancellationToken);

        if (!result)
        {
            throw new RpcException(new Status(StatusCode.Unknown, "An error occurred"));
        }

        return new();
    }

    private async Task ValidateUserAccessRequirement(ServerCallContext context, int userId)
    {
        AuthorizationResult authResult = await _authorizationService
            .AuthorizeAsync(context.GetHttpContext().User, userId, new UserAccessRequirement(userId));

        if (!authResult.Succeeded)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, "User does not have the required access."));
        }
    }

    [AllowAnonymous]
    public override async Task<Empty> CreateUserHashedPassword(CreateUserHashedPasswordRequest request, ServerCallContext context)
    {
        // await ValidateUserAccessRequirement(context, request.UserId);
        // string username = context.GetHttpContext().User.FindFirst(ClaimTypes.Name)?.Value ?? throw new ArgumentException("Username cannot be null");

        string username = "test";
        var command = new CreateUpdateUserPasswordCommand(request.UserId, username, request.HashedPassword);

        await _mediator.Send(command, context.CancellationToken);

        return new();
    }

    [AllowAnonymous]
    public override async Task<UserExtended> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var command = new CreateUserCommand(
            request.Name,
            request.Address,
            request.ImageUrl,
            request.PhoneNumber,
            request.HomeNumber,
            request.Surname,
            request.Username);

        var user = await _mediator.Send(command, context.CancellationToken);

        return new()
        {
            Id = user.Id,
            Username = user.Username,
            Surname = user.Surname,
            PhoneNumber = user.UserDetails.PersonalNumber,
            HomeNumber = user.UserDetails.HomeNumber,
            ImageUrl = user.UserDetails.ImageUrl,
            Name = user.Name,
            Address = user.UserDetails.Address,
        };
    }

    [AllowAnonymous]
    public override async Task<User> GetUser(GetUserRequest request, ServerCallContext context)
    {
        UserPreview? user = request.IdentifierCase switch
        {
            GetUserRequest.IdentifierOneofCase.Id => await _userQueries.GetUserAsync(request.Id),
            GetUserRequest.IdentifierOneofCase.Username => await _userQueries.GetUserAsync(request.Username),
            _ => throw new RpcException(new Status(StatusCode.InvalidArgument, "Need to specfiy at least one identifier to retrieve user details")),
        };

        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found."));
        }

        return new()
        {
            Id = user.Id,
            Username = user.Username,
            Surname = user.Surname,
            Name = user.Name,
        };
    }

    [AllowAnonymous]
    public override async Task<UserExtended> GetUserExtended(GetUserRequest request, ServerCallContext context)
    {
        UserExtendedPreview? user = request.IdentifierCase switch
        {
            GetUserRequest.IdentifierOneofCase.Id => await _userQueries.GetUserExtendedAsync(request.Id),
            GetUserRequest.IdentifierOneofCase.Username => await _userQueries.GetUserExtendedAsync(request.Username),
            _ => throw new RpcException(new Status(StatusCode.InvalidArgument, "Need to specfiy at least one identifier to retrieve user details")),
        };

        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found."));
        }

        return new()
        {
            Id = user.Id,
            Username = user.Username,
            Surname = user.Surname,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
            HomeNumber = user.HomeNumber,
            Address = user.Address,
        };
    }
}


