using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Services;
using MovieShowcaseSPA.Enums;
using static MovieSystem.V1.Movies;

namespace MoveShowcaseDDD.Areas.Movie.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
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
    public async Task<object> Movies(bool extended = false)
    {
        if (extended)
        {
            _logger.LogInformation("Retrieving extended movies.");
            MovieSystem.V1.ListExtendedMoviesResponse? extendedMovie = await _movieClient
                .ListExtendedMoviesAsync(new()
                {
                    UserId = _userService.GetUserId(),
                });
            return extendedMovie.Movies.ToArray();
        }

        MovieSystem.V1.ListMoviesResponse movies = await _movieClient
            .ListMoviesAsync(new());

        return movies.Movies.ToArray();
    }

    [HttpGet]
    public async Task<object> Movie(int id, bool extended = false)
    {
        if (extended)
        {
            var movie = await _movieClient
                 .GetMovieAsync(new()
                 {
                     Id = id,
                 });

            return new
            {
                movie.Id,
                movie.Title,
            };
        }

        var extendedMovie = await _movieClient.GetMovieExtendedAsync(new()
        {
            Id = id,
            UserId = _userService.GetUserId(),
        });

        return new
        {
            extendedMovie.Title,
            extendedMovie.Id,
        };
    }

    public record Favourite(int? Id, Rating Rating, bool Liked, string Reason);
    [HttpPut]
    public async Task<IActionResult> ToggleFavourite([FromBody] Favourite data)
    {
        _ = data;
        await Task.CompletedTask;
        return NoContent();
    }

}

