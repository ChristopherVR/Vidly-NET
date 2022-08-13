using Microsoft.Data.SqlClient;

namespace MovieSystem.API.Application.Queries;
public class GenreQueries : IGenreQueries
{
    private readonly string _connectionString;

    public GenreQueries(string connectionString) => _connectionString = !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentNullException(nameof(connectionString));

    public async Task<List<GenrePreview>> ListGenresAsync(string? searchTerm)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var sql = @"
                        SELECT
                          [Id],
                          [Name]
                    FROM
                         [Movie].[Genres]
                    WHERE @searchTerm IS NULL OR [Name] LIKE CONCAT('%', @searchTerm, '%');";

        return (await connection.QueryAsync<GenrePreview>(
            sql, new { searchTerm })).AsList();
    }
}
