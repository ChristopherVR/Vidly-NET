using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Services;
using MovieShowcaseSPA.Enums;
using static MovieSystem.V1.Movies;
using static UserSystem.V1.Users;

namespace MoveShowcaseDDD.Areas.Controllers;
[Authorize]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MoviesClient _movieClient;
    private readonly ILogger<MoviesController> _logger;
    private readonly IUserService _userService;
    public MoviesController(MoviesClient client, IUserService userService, ILogger<MoviesController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _movieClient = client ?? throw new ArgumentNullException(nameof(client));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
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

    [HttpGet("movie/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<object> Movie(uint id, bool extended = false)
    {
        if (id == default)
        {
            return BadRequest();
        }

        if (extended)
        {
            MovieSystem.V1.Movie movie = await _movieClient
                 .GetMovieAsync(new()
                 {
                     Id = (int)id,
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

        MovieSystem.V1.MovieExtended extendedMovie = await _movieClient.GetMovieExtendedAsync(new()
        {
            Id = (int)id,
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

    [HttpDelete("movie/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Movie(uint id)
    {
        if (id == default)
        {
            return BadRequest();
        }

        await _movieClient.DeleteMovieAsync(new()
        {
            Id = (int)id,
        });

        return NoContent();
    }

    public record MovieDTO(int DailyRentalRate, int GenreId, bool Liked, int NumberInStock, int Rating, string Title, string? ImdbUrl);
    [HttpPost("movie/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateMovie([FromBody] MovieDTO data)
    {
        if (data.Title == default)
        {
            return BadRequest();
        }

        var res = await _movieClient.CreateMovieAsync(new()
        {
            DailyRentalRate = data.DailyRentalRate,
            GenreId = data.GenreId,
            NumberInStock = data.NumberInStock,
            Rating = data.Rating,
            ImdbUrl = data.ImdbUrl,
            Title = data.Title,
        });

        return Ok(new
        {
            res.Rating,
            res.Id,
            res.Genre,
            res.Title,
            res.DailyRentalRate,
            res.NumberInStock,
        });
    }

    [HttpPatch("movie/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateMovie(uint id, [FromBody] MovieDTO data)
    {
        if (data.Title == default || id == default)
        {
            return BadRequest();
        }

        await _movieClient.UpdateMovieAsync(new()
        {
            Id = (int) id,
            DailyRentalRate = data.DailyRentalRate,
            GenreId = data.GenreId,
            NumberInStock = data.NumberInStock,
            Rating = data.Rating,
            ImdbUrl = data.ImdbUrl,
            Title = data.Title,
        });

        return NoContent();
    }

    public record Favourite(Rating Rating, bool Liked, string Reason);
    [HttpPut("movie/favourite/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ToggleFavourite([FromServices] UsersClient usersClient, uint id, [FromBody] Favourite data)
    {
        if (id == default)
        {
            return BadRequest();
        }

        await usersClient.ToggleUserFavouriteMovieAsync(new()
        {
            UserId = _userService.GetUserId(),
            Liked = data.Liked,
            MovieId = (int)id,
            Rating = (int)data.Rating,
            Reason = data.Reason,
        });

        return NoContent();
    }

}
