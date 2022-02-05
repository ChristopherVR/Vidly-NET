using Dapper;
using Microsoft.Data.SqlClient;

namespace MovieSystem.API.Application.Queries;
public class UserQueries : IUserQueries
{
    private readonly string _connectionString;

    public UserQueries(string connectionString)
    {
        _connectionString = !string.IsNullOrWhiteSpace(connectionString) 
            ? connectionString 
            : throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task<UserPreview?> GetUserAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
                        SELECT
                          [Id],
                          [Name],
                          [Surname],
                          [Username]
                    FROM
                         [Movie].[Users] s";

        var user = await connection.QueryFirstOrDefaultAsync<UserPreview>(
            sql);

        return user;
    }

    public Task<UserPreview?> GetUserAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<UserExtendedPreview?> GetUserExtendedAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
                        SELECT
                          s.[Id]        [Id],
                          s.[Name]      [Name],
                          s.[Surname]   [Surname],
                          s.[Username]  [Username],
                          ufm.[MovieId] [MovieId],
                          ufm.[Reason]  [Reason],
                          ufm.[Rating]  [Rating]
                    FROM
                         [Movie].[Users] s
                    LEFT JOIN [Movie].[UserFavouriteMovies] ufm
                    ON ufm.UserId = s.Id";

        IEnumerable<Tuple<UserExtendedPreview, UserFavouriteMovie>> user = await connection
            .QueryAsync<UserExtendedPreview, UserFavouriteMovie, Tuple<UserExtendedPreview, UserFavouriteMovie>> (
            sql,
            (u, ufm) => new (u, ufm),
            param: new { id },
            splitOn: "MovieId");

        return user
                .GroupBy(t => t.Item1.Id)
                .Select(g =>
                {
                    UserExtendedPreview user = g.First().Item1;
                    if (g.Any(o => o.Item2 != null))
                    {
                        UserFavouriteMovie[] favouriteMovies = g
                            .GroupBy(o => o.Item2?.MovieId)
                            .Select(movie => movie.First().Item2)
                            .ToArray();
                        user = user with { UserFavouriteMovies = favouriteMovies };
                    }
                    return user;
                })
                .FirstOrDefault();
    }

    public Task<UserExtendedPreview?> GetUserExtendedAsync(string username)
    {
        throw new NotImplementedException();
    }
}


