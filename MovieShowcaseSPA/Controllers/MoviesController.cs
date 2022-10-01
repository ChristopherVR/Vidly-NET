using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Models;
using MoveShowcaseDDD.Services;
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
    private readonly UsersClient _usersClient;

    public MoviesController(
        MoviesClient client,
        UsersClient usersClient,
        IUserService userService,
        ILogger<MoviesController> logger)
    {
        _logger = logger;
        _movieClient = client;
        _usersClient = usersClient;
        _userService = userService;
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
            return extendedMovie.Movies.Select(movie => new
            {
                movie.Id,
                movie.Title,
                movie.NumberInStock,
                Genre = new
                {
                    Value = movie.Genre.Id,
                    Label = movie.Genre.Name,
                },
                movie.UpdatedDate,
                movie.DailyRentalRate,
                movie.Rating,
                movie.Liked,
            });
        }

        MovieSystem.V1.ListMoviesResponse movies = await _movieClient
            .ListMoviesAsync(new()
            {
                UserId = _userService.GetUserId(),
            });

        return movies.Movies.Select(movie => new
        {
            movie.Id,
            movie.Title,
            movie.NumberInStock,
            Genre = new
            {
                Value = movie.Genre.Id,
                Label = movie.Genre.Name,
            },
            movie.DailyRentalRate,
            movie.Rating,
        });
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
                Genre = new
                {
                    Value = movie.Genre.Id,
                    Label = movie.Genre.Name,
                },
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
            Genre = new
            {
                Value = extendedMovie.Genre?.Id,
                Label = extendedMovie.Genre?.Name,
            },
            UpdatedDate = extendedMovie.UpdatedDate.ToDateTime(),
            extendedMovie.DailyRentalRate,
            extendedMovie.Rating,
            extendedMovie.Liked,
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

        MovieSystem.V1.Movie? res = await _movieClient.CreateMovieAsync(new()
        {
            DailyRentalRate = data.DailyRentalRate,
            GenreId = data.GenreId,
            NumberInStock = data.NumberInStock,
            Rating = data.Rating,
            ImdbUrl = data.ImdbUrl?.ToString(),
            Title = data.Title,
        });

        await _usersClient.ToggleUserFavouriteMovieAsync(new()
        {
            Liked = data.Liked,
            MovieId = res.Id,
            UserId = _userService.GetUserId(),
        });

        return Ok(new
        {
            res.Rating,
            res.Id,
            Genre = new
            {
                Value = res.Genre.Id,
                Label = res.Genre.Name,
            },
            res.Title,
            res.DailyRentalRate,
            res.NumberInStock,
            data.Liked,
        });
    }

    [HttpPatch("movie/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Movie(uint id, [FromBody] MovieDTO data)
    {
        if (data.Title == default || id == default)
        {
            return BadRequest();
        }

        await _movieClient.UpdateMovieAsync(new()
        {
            Id = (int)id,
            DailyRentalRate = data.DailyRentalRate,
            GenreId = data.GenreId,
            NumberInStock = data.NumberInStock,
            Rating = data.Rating,
            ImdbUrl = data.ImdbUrl?.ToString(),
            Title = data.Title,
        });

        return NoContent();
    }

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
        });

        return NoContent();
    }
}
