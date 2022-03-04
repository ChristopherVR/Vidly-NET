using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoveShowcaseDDD.Areas.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class GenreController : ControllerBase
{
    private readonly GenreSystem.V1.Genres.GenresClient _genresClient;
    private readonly ILogger<GenreController> _logger;
    public GenreController(GenreSystem.V1.Genres.GenresClient genresClient, ILogger<GenreController> logger)
    {
        _genresClient = genresClient ?? throw new ArgumentNullException(nameof(genresClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Route("genres")]
    public async Task<IActionResult> Genres(string? searchTerm)
    {
        var genres = await _genresClient.ListGenresAsync(new()
        {
            SearchTerm = searchTerm,
        });

        return Ok(genres.Genres);
    }
}

