using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MovieSystem.API.Application.Queries;
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


        return new ()
        {
            Address = request.Address,
            HomeNumber = request.HomeNumber,
            Id = request.Id,
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Surname = request.Surname,
            // Username = request.Username,
        };
    }

    public override async Task<Empty> ToggleUserFavouriteMovie(ToggleUserFavouriteMovieRequest request, ServerCallContext context)
    {
        var command = new ToggleUserFavouriteMovieCommand(request.UserId, request.MovieId, (Domain.AggregatesModel.UserAggregate.Rating) request.Rating, "editedUser", request.Reason);

        bool result = await _mediator.Send(command, context.CancellationToken);

        if (!result)
        {
            throw new RpcException(new Status(StatusCode.Unknown, "An error occurred"));
        }

        return new Empty();
    }

    // public override async Task<User> CreateUser(CreateUserRequest request, ServerCallContext context)
    // {
    //     bool result = await _mediator.Send(command, context.CancellationToken);
    // 
    //     if (!result)
    //     {
    //         throw new RpcException(new Status(StatusCode.Unknown, "An error occurred"));
    //     }
    // 
    //     return new User
    //     {
    // 
    //     };
    // }

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

        return new ()
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


