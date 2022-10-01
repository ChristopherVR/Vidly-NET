using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoveShowcaseDDD.Areas.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
[ApiConventionType(typeof(DefaultApiConventions))]
public class GenresController : ControllerBase
{
    private readonly GenreSystem.V1.Genres.GenresClient _genresClient;
    public GenresController(GenreSystem.V1.Genres.GenresClient genresClient)
        => _genresClient = genresClient;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Genres(string? searchTerm)
    {
        GenreSystem.V1.ListGenresResponse genres = await _genresClient.ListGenresAsync(new()
        {
            SearchTerm = searchTerm,
        });

        return Ok(genres.Genres);
    }
}
