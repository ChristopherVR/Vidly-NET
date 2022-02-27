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

    public record ToggleUserFavouriteMovieCommand(
        int UserId,
        int MovieId,
        Domain.AggregatesModel.UserAggregate.Rating Rating,
        string User,
        string Reason) : IRequest<bool>;
}