using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Services;
using MovieShowcaseSPA.Enums;
using static MovieSystem.V1.Movies;
using static UserSystem.V1.Users;

namespace MoveShowcaseDDD.Areas.Controllers;
// TODO: fix endpoint routes
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
            .ListMoviesAsync(new()
            {
                UserId = _userService.GetUserId(),
            });

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
                movie.NumberInStock,
                movie.Genre,
                movie.DailyRentalRate,
                movie.Rating,
            };
        }

        var extendedMovie = await _movieClient.GetMovieExtendedAsync(new()
        {
            Id = id,
            UserId = _userService.GetUserId(),
        });

        return new
        {
            extendedMovie.Id,
            extendedMovie.Title,
            extendedMovie.NumberInStock,
            extendedMovie.Genre,
            extendedMovie.UpdatedDate,
            extendedMovie.DailyRentalRate,
            extendedMovie.Rating,
            extendedMovie.Reason,
        };
    }

    public record Favourite(int Id, Rating Rating, bool Liked, string Reason);
    [HttpPut]
    public async Task<IActionResult> ToggleFavourite([FromServices] UsersClient usersClient, [FromBody] Favourite data)
    {
        await usersClient.ToggleUserFavouriteMovieAsync(new()
        {
            UserId = _userService.GetUserId(),
            Liked = data.Liked,
            MovieId = data.Id,
            Rating = (int)data.Rating,
            Reason = data.Reason,
        });

        return NoContent();
    }

}
