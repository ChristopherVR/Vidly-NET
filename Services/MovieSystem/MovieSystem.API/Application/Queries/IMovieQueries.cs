namespace MovieSystem.API.Application.Queries;

public interface IMovieQueries
{
    Task<MoviePreview?> GetMovieAsync(int id);
    Task<List<MoviePreview>> GetMoviesAsync();
    Task<MovieExtendedPreview?> GetMovieExtendedAsync(int id, int userId);
    Task<List<MovieExtendedPreview>> GetMoviesExtendedAsync(int userId);
}

