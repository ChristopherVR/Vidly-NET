using GenreSystem.V1;
using MovieSystem.API.Application.Queries;

namespace MovieSystem.API.Grpc;

[Authorize]
public class GenresServiceV1 : Genres.GenresBase
{
    private readonly IGenreQueries _genreQueries;

    public GenresServiceV1(ILogger<GenresServiceV1> logger, IGenreQueries genreQueries) => _genreQueries = genreQueries ?? throw new ArgumentNullException(nameof(genreQueries));

    public override async Task<ListGenresResponse> ListGenres(ListGenresRequest request, ServerCallContext context)
    {
        var genres = await _genreQueries.ListGenresAsync(request.SearchTerm);

        return new()
        {
            Genres =
            {
                genres.Select(genre => new ListGenresResponse.Types.Genre()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                }),
            },
        };
    }
}


