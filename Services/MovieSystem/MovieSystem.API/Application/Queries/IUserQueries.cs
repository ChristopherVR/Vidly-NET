namespace MovieSystem.API.Application.Queries;

public interface IUserQueries
{
    Task<UserPreview?> GetUserAsync(int id);
    Task<UserPreview?> GetUserAsync(string username);
    Task<UserExtendedPreview?> GetUserExtendedAsync(int id);
    Task<UserExtendedPreview?> GetUserExtendedAsync(string username);
}

