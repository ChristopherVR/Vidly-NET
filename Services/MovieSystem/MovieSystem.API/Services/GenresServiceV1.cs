using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MovieSystem.API.Application.Queries;
using GenreSystem.V1;
using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Grpc;

[Authorize]
public class GenresServiceV1 : Genre.GenreBase
{
    private readonly ILogger<GenresServiceV1> _logger;
    private readonly IGenreQueries _genreQueries;

    public GenresServiceV1(ILogger<UsersServiceV1> logger, IGenreQueries genreQueries)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _genreQueries = genreQueries ?? throw new ArgumentNullException(nameof(genreQueries));
    }

    public override async Task<ListGenresResponse> ListGenres(ListGenresRequest request, ServerCallContext context)
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


