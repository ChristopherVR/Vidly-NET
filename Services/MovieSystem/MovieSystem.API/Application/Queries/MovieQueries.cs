using Dapper;
using Microsoft.Data.SqlClient;

namespace MovieSystem.API.Application.Queries;
public class MovieQueries : IMovieQueries
{
    private readonly string _connectionString;

    public MovieQueries(string connectionString)
    {
        _connectionString = !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentNullException(nameof(connectionString));
    }

    #region Movies

    public async Task<MoviePreview?> GetMovieAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
                SELECT
                    m.[Id]                         [Id],
                    m.[Name]                       [Name],
                    m.[Description]                [Description],
                    m.[ImdbUrl]                    [ImdbUrl]
                FROM Movie.Movies m WITH(NOLOCK)
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

        var sql = @"
                SELECT
                    m.[Id]                         [Id],
                    m.[Name]                       [Name],
                    m.[Description]                [Description],
                    m.[ImdbUrl]                    [ImdbUrl],
                    ufm.[Reason]                   [Reason],
                    ufm.[Rating]                   [Rating]
                FROM Movie.Movies m
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

        var sql = @"
                SELECT
                    m.[Id]                         [Id],
                    m.[Name]                       [Name],
                    m.[Description]                [Description],
                    m.[ImdbUrl]                    [ImdbUrl]
                FROM Movie.Movies m";

        IEnumerable<MoviePreview> movies = await connection.QueryAsync<MoviePreview>(
            sql);

        return movies.AsList();
    }

    public async Task<List<MovieExtendedPreview>> GetMoviesExtendedAsync(int userId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
                SELECT
                    m.[Id]                         [Id],
                    m.[Name]                       [Name],
                    m.[Description]                [Description],
                    m.[ImdbUrl]                    [ImdbUrl],
                    ufm.[Reason]                   [Reason],
                    ufm.[Rating]                   [Rating]
                FROM Movie.Movies m
                LEFT JOIN Movie.UserFavouriteMovies ufm
                ON ufm.MovieId = m.Id
                AND ufm.UserId = @userId";

        IEnumerable<MovieExtendedPreview> movies = await connection.QueryAsync<MovieExtendedPreview>(sql, new { userId });

        return movies.AsList();
    }

    #endregion Movies

}
