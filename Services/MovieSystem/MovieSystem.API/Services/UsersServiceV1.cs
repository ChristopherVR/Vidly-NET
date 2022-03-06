using Google.Protobuf.WellKnownTypes;
using MovieSystem.API.Application.Queries;
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

    public UsersServiceV1(ILogger<UsersServiceV1> logger, IMediator mediator, IUserQueries userQueries)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
    }

    public override async Task<UserExtended> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
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
        };
    }

    public override async Task<Empty> ToggleUserFavouriteMovie(ToggleUserFavouriteMovieRequest request, ServerCallContext context)
    {
        string username = context.GetHttpContext().User.FindFirst(ClaimTypes.Name)?.Value ?? throw new ArgumentNullException("Username cannot be null");

        var command = new ToggleUserFavouriteMovieCommand(
            request.UserId,
            request.MovieId,
            (Domain.AggregatesModel.UserAggregate.Rating)request.Rating,
            username,
            request.Reason,
            request.Liked);

        bool result = await _mediator.Send(command, context.CancellationToken);

        if (!result)
        {
            throw new RpcException(new Status(StatusCode.Unknown, "An error occurred"));
        }

        return new();
    }

    public override async Task<UserExtended> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var command = new CreateUserCommand(
            request.Name,
            request.Password,
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

    public override async Task<User> GetUser(GetUserRequest request, ServerCallContext context)
    {
        UserPreview? user = request.IdentifierCase switch
        {
            GetUserRequest.IdentifierOneofCase.Id => await _userQueries.GetUserAsync(request.Id),
            GetUserRequest.IdentifierOneofCase.Username => await _userQueries.GetUserAsync(request.Username),
            _ => throw new NotImplementedException(),
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

    public override async Task<UserExtended> GetUserExtended(GetUserRequest request, ServerCallContext context)
    {
        UserExtendedPreview? user = request.IdentifierCase switch
        {
            GetUserRequest.IdentifierOneofCase.Id => await _userQueries.GetUserExtendedAsync(request.Id),
            GetUserRequest.IdentifierOneofCase.Username => await _userQueries.GetUserExtendedAsync(request.Username),
            _ => throw new NotImplementedException(),
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


