namespace MovieSystem.API.Application.Commands;
public class UserCommands
{
    public record UpdateUserCommand(
        int Id,
        string Name,
        string Address,
        string? ImageUrl,
        string PhoneNumber,
        string HomeNumber,
        string Surname,
        string User) : IRequest<bool>;

    public record ToggleUserFavouriteMovieCommand(
        int UserId,
        int MovieId,
        Domain.AggregatesModel.UserAggregate.Rating Rating,
        string User,
        string Reason) : IRequest<bool>;
}