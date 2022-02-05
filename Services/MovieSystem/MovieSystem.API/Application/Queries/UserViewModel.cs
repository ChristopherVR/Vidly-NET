using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.API.Application.Queries;

public record UserPreview(int Id, string Name, string Surname, string Username);
public record UserFavouriteMovie(int MovieId, string Reason, Rating Rating);
public record UserExtendedPreview(
    int Id, 
    string Name, 
    string Surname, 
    string Username, 
    string? ImageUrl, 
    string Address,
    string HomeNumber, 
    string PhoneNumber,
    UserFavouriteMovie[]? UserFavouriteMovies);

