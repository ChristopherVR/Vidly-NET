namespace MovieShowcaseSPA.Models;

public record Favourite(bool Liked);
public record MovieDTO(int DailyRentalRate, int GenreId, bool Liked, int NumberInStock, int Rating, string Title, Uri? ImdbUrl);
