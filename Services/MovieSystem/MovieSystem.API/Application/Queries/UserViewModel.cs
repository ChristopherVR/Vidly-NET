namespace MovieSystem.API.Application.Queries;

public record UserPreview(int Id, string Name, string Surname, string Username);
public record UserFavouriteMovie(int MovieId, bool Liked);
public record UserExtendedPreview(
    int Id, 
    string Name, 
    string Surname, 
    string Username, 
    string ImageUrl, 
    string Address,
    string HomeNumber, 
    string PhoneNumber,
    string HashedPassword
    )
{
    public UserFavouriteMovie[]? UserFavouriteMovies { get; init; }
};

