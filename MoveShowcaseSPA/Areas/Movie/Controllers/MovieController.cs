using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Services;
using static MovieSystem.V1.Movies;

namespace MoveShowcaseDDD.Areas.Movie.Controllers;
[Authorize]
[ApiController]
public class MovieController : Controller
{
    private readonly MoviesClient _movieClient;
    private readonly ILogger<MovieController> _logger;
    private readonly IUserService _userService;
    public MovieController(MoviesClient client, IUserService userService, ILogger<MovieController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieClient = client ?? throw new ArgumentNullException(nameof(client));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    public async Task<IActionResult> Movies(bool extended = false)
    {
        if (extended)
        {
            _logger.LogInformation("Retrieving extended movies.");
            MovieSystem.V1.ListExtendedMoviesResponse? extendedMovie = await _movieClient
                .ListExtendedMoviesAsync(new()
                {
                    UserId = _userService.GetUserId(),
                });
            return Json(data: extendedMovie.Movies.Select(x => new
            {
                x.Id,
                x.ImdbUrl,
                x.Name,
                x.Description,
                x.UpdatedDate,
                x.Reason,
                x.Rating,
            }).ToArray());
        }

        MovieSystem.V1.ListMoviesResponse movies = await _movieClient
            .ListMoviesAsync(new());
        return Json(data: movies.Movies.Select(x => new
        {
            x.Id,
            x.ImdbUrl,
            x.Name,
            x.Description,
        }).ToArray());
    }

    [HttpGet]
    public async Task<IActionResult> Movie(int id, bool extended = false)
    {
        if (extended)
        {
            var movie = await _movieClient
                 .GetMovieAsync(new()
                 {
                     Id = id,
                 });

            return Json(data: new
            {
                movie.Name,
                movie.Id,
                movie.Description,
                movie.ImdbUrl,
            });
        }

        var extendedMovie = await _movieClient.GetMovieExtendedAsync(new()
        {
            Id = id,
            UserId = _userService.GetUserId(),
        });

        return Json(data: new
        {
            extendedMovie.Name,
            extendedMovie.Id,
            extendedMovie.Description,
            extendedMovie.ImdbUrl,
        });
 
    }
    [HttpPut]
    public async Task<IActionResult> ToggleFavourite(int id)
    {
        _ = id;
        await Task.CompletedTask;
        return NoContent();
    }

}

