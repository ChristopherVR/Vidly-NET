using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.API.Application.Queries;

public record MoviePreview (int Id, string Name, string Description, string ImdbUrl);

public record MovieExtendedPreview(int Id, string Name, string Description, string ImdbUrl, string? Reason, Rating? Rating, DateTime UpdatedDate);

