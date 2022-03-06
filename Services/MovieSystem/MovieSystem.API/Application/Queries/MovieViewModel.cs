namespace MovieSystem.API.Application.Queries;

public record MoviePreview (
    int Id,
    string Title,
    int NumberInStock,
    int Rating,
    string Genre,
    int DailyRentalRate);

public record MovieExtendedPreview(
    int Id, 
    string Title, 
    int NumberInStock, 
    int DailyRentalRate, 
    int Rating, 
    string Genre, 
    bool Liked, 
    string ImdbUrl, 
    string Reason, 
    DateTime UpdatedDate,
    int UserRating);
