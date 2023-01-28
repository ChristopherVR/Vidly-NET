using Microsoft.Data.SqlClient;

namespace MovieSystem.API.Application.Queries;
public class UserQueries : IUserQueries
{
    private readonly string _connectionString;

    public UserQueries(string connectionString) => _connectionString = !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentNullException(nameof(connectionString));

    public async Task<UserPreview?> GetUserAsync(string username) => await GetUserAsync(null, username);
    public async Task<UserPreview?> GetUserAsync(int id) => await GetUserAsync(id, null);

    private async Task<UserPreview?> GetUserAsync(int? id, string? username)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string sql = @"
                        SELECT
                          [Id],
                          [Name],
                          [Surname],
                          [Username]
                    FROM
                         [Movie].[Users] s
                    WHERE (@id IS NULL OR Id = @id)
                    AND (@username IS NULL OR Username = @username)";

        UserPreview? user = await connection.QueryFirstOrDefaultAsync<UserPreview>(
            sql, new { id, username });

        return user;
    }

    public async Task<UserExtendedPreview?> GetUserExtendedAsync(int id) => await GetUserExtendedAsync(id, null);

    private async Task<UserExtendedPreview?> GetUserExtendedAsync(int? id, string? username)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string sql = @"
                        SELECT
                          s.[Id]                            [Id],
                          s.[Name]                          [Name],
                          s.[Surname]                       [Surname],
                          s.[Username]                      [Username],
                          s.[UserDetails_ImageUrl]          [ImageUrl],
                          s.[UserDetails_Address]           [Address],
                          s.[UserDetails_HomeNumber]        [HomeNumber],
                          s.[UserDetails_PersonalNumber]    [PhoneNumber],
                          s.[HashedPassword]                [HashedPassword],
                          ufm.[MovieId]                     [MovieId],
                          ufm.[Liked]                       [Liked]
                    FROM
                         [Movie].[Users] s
                    LEFT JOIN [Movie].[UserFavouriteMovies] ufm
                    ON ufm.UserId = s.Id
                    WHERE (@id IS NULL OR s.Id = @id)
                    AND (@username IS NULL OR s.Username = @username)";

        IEnumerable<Tuple<UserExtendedPreview, UserFavouriteMovie>> user = await connection
            .QueryAsync<UserExtendedPreview, UserFavouriteMovie, Tuple<UserExtendedPreview, UserFavouriteMovie>>(
            sql,
            (u, ufm) => new(u, ufm),
            param: new { id, username },
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

    public async Task<UserExtendedPreview?> GetUserExtendedAsync(string username) => await GetUserExtendedAsync(null, username);
}
