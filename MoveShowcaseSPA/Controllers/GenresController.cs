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
    private readonly ILogger<GenresController> _logger;
    public GenresController(GenreSystem.V1.Genres.GenresClient genresClient, ILogger<GenresController> logger)
    {
        _genresClient = genresClient ?? throw new ArgumentNullException(nameof(genresClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

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

