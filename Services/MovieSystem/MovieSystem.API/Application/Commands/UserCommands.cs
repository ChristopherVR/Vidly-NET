using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.API.Application.Commands;
public class UserCommands
{

    public record CreateUserCommand(
        string Name,
        string Address,
        string? ImageUrl,
        string PhoneNumber,
        string HomeNumber,
        string Surname,
        string UserName) : IRequest<User>;

    public record UpdateUserCommand(
        int Id,
        string Name,
        string Address,
        string? ImageUrl,
        string PhoneNumber,
        string HomeNumber,
        string Surname,
        string User) : IRequest<User>;

    public record CreateUpdateUserPasswordCommand(int Id, string User, string HashedPassword) : IRequest<bool>;

    public record ToggleUserFavouriteMovieCommand(
        int UserId,
        int MovieId,
        string User,
        bool Liked) : IRequest<bool>;
}