using Microsoft.Data.SqlClient;

namespace MovieSystem.API.Application.Queries;
public class MovieQueries : IMovieQueries
{
    private readonly string _connectionString;

    public MovieQueries(string connectionString) => _connectionString = !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentNullException(nameof(connectionString));

    #region Movies

    public async Task<MoviePreview?> GetMovieAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string sql = @"
                SELECT
                    m.[Id]                                 [Id],
                    m.[Title]                              [Title],
                    m.[NumberInStock]                      [NumberInStock],
                    m.[Rating]                             [Rating],
                    g.[Id]                                 [GenreId],
                    g.[Name]                               [GenreName],
                    m.[DailyRentalRate]                    [DailyRentalRate]
                FROM Movie.Movies m
                JOIN Movie.Genres g
                ON g.Id = m.GenreId
                WHERE m.Id = @id";

        MoviePreview? movie = await connection.QueryFirstOrDefaultAsync<MoviePreview>(
            sql,
            param: new
            {
                id,
            });

        return movie;
    }

    public async Task<MovieExtendedPreview?> GetMovieExtendedAsync(int id, int userId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string sql = @"
                SELECT
                    m.[Id]                                 [Id],
                    m.[Title]                              [Title],
                    m.[NumberInStock]                      [NumberInStock],
                    m.[DailyRentalRate]                    [DailyRentalRate],
                    m.[Rating]                             [Rating],
                    g.[Id]                                 [GenreId],
                    g.[Name]                               [GenreName],
                    ISNULL(ufm.[Liked], 0)                 [Liked],
                    m.[ImdbUrl]                            [ImdbUrl],
                    m.UpdatedDate                          [UpdatedDate]
                FROM Movie.Movies m
                JOIN Movie.Genres g
                ON g.Id = m.GenreId
                LEFT JOIN Movie.UserFavouriteMovies ufm
                ON ufm.MovieId = m.Id
                AND ufm.UserId = @userId
                WHERE m.Id = @id";

        MovieExtendedPreview? movie = await connection.QueryFirstOrDefaultAsync<MovieExtendedPreview>(
            sql,
            param: new
            {
                id,
                userId,
            });

        return movie;
    }

    public async Task<List<MoviePreview>> GetMoviesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string sql = @"
                SELECT
                    m.[Id]                                 [Id],
                    m.[Title]                              [Title],
                    m.[NumberInStock]                      [NumberInStock],
                    m.[Rating]                             [Rating],
                    g.[Id]                                 [GenreId],
                    g.[Name]                               [GenreName],
                    m.[DailyRentalRate]                    [DailyRentalRate]
                FROM Movie.Movies m
                JOIN Movie.Genres g
                ON g.Id = m.GenreId";

        IEnumerable<MoviePreview> movies = await connection.QueryAsync<MoviePreview>(
            sql);

        return movies.AsList();
    }

    public async Task<List<MovieExtendedPreview>> GetMoviesExtendedAsync(int userId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string sql = @"
                SELECT
                    m.[Id]                                 [Id],
                    m.[Title]                              [Title],
                    m.[NumberInStock]                      [NumberInStock],
                    m.[DailyRentalRate]                    [DailyRentalRate],
                    m.[Rating]                             [Rating],
                    g.[Id]                                 [GenreId],
                    g.[Name]                               [GenreName],
                    ISNULL(ufm.[Liked], 0)                 [Liked],
                    m.[ImdbUrl]                            [ImdbUrl],
                    m.UpdatedDate                          [UpdatedDate]
                FROM Movie.Movies m
                JOIN Movie.Genres g
                ON g.Id = m.GenreId
                LEFT JOIN Movie.UserFavouriteMovies ufm
                ON ufm.MovieId = m.Id
                AND ufm.UserId = @userId";

        IEnumerable<MovieExtendedPreview> movies = await connection.QueryAsync<MovieExtendedPreview>(sql, new { userId });

        return movies.AsList();
    }

    #endregion Movies

}
