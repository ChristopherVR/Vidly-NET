namespace MovieSystem.API.Application.Queries;

public record MoviePreview(
    int Id,
    string Title,
    int NumberInStock,
    int? Rating,
    int GenreId,
    string GenreName,
    int DailyRentalRate);

public record MovieExtendedPreview(
    int Id,
    string Title,
    int NumberInStock,
    int DailyRentalRate,
    int? Rating,
    int GenreId,
    string GenreName,
    bool Liked,
    string ImdbUrl,
    DateTime UpdatedDate);
