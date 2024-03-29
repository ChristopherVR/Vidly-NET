﻿using Google.Protobuf.WellKnownTypes;
using MovieSystem.API.Application.Behaviors;
using MovieSystem.API.Application.Queries;
using MovieSystem.V1;
using System.Security.Claims;
using static MovieSystem.API.Application.Commands.MovieCommands;

namespace MovieSystem.API.Services;
[Authorize]
public class MoviesServiceV1 : Movies.MoviesBase
{
    private readonly ILogger<MoviesServiceV1> _logger;
    private readonly IMediator _mediator;
    private readonly IMovieQueries _movieQueries;
    private readonly IGenreQueries _genreQueries;

    public MoviesServiceV1(ILogger<MoviesServiceV1> logger, IMediator mediator, IMovieQueries movieQueries, IGenreQueries genreQueries)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _movieQueries = movieQueries ?? throw new ArgumentNullException(nameof(movieQueries));
        _genreQueries = genreQueries ?? throw new ArgumentNullException(nameof(genreQueries));
    }

    #region Movies

    public override async Task<Empty> DeleteMovie(DeleteMovieRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Deleting movie with Id - {id}", request.Id);
        bool result = await _mediator.Send(new DeleteMovieCommand(request.Id), context.CancellationToken);

        if (!result)
        {
            throw new RpcException(new Status(StatusCode.Internal, "An error occurred trying to delete the movie."));
        }

        return new();
    }

    public override async Task<Movie> CreateMovie(CreateMovieRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Creating movie with Title - {title}", request.Title);
        string username = context.GetHttpContext().User.FindFirst(ClaimTypes.Name)?.Value ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Username cannot be null"));

        var command = new CreateMovieCommand(
            request.Title,
            request.NumberInStock,
            request.Rating,
            request.GenreId,
            request.DailyRentalRate,
            username,
            request.ImdbUrl);

        var movie = await _mediator.Send(command, context.CancellationToken);

        var genreName = await _genreQueries.GetGenreNameAsync(movie.GenreId);
        return new()
        {
            DailyRentalRate = movie.DailyRentalRate,
            Genre = new()
            {
                Id = movie.GenreId,
                Name = genreName,
            },
            Id = movie.Id,
            NumberInStock = movie.NumberInStock,
            Rating = movie.Rating,
            Title = movie.Title,
        };
    }

    public override async Task<Movie> UpdateMovie(UpdateMovieRequest request, ServerCallContext context)
    {
        string username = context.GetHttpContext().User.FindFirst(ClaimTypes.Name)?.Value ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Username cannot be null"));

        var command = new UpdateMovieCommand(
            request.Id,
            request.Title,
            request.NumberInStock,
            request.Rating,
            request.GenreId,
            request.DailyRentalRate,
            username,
            request.ImdbUrl);

        var movie = await _mediator.Send(command, context.CancellationToken);

        var genreName = await _genreQueries.GetGenreNameAsync(movie.GenreId);
        return new()
        {
            DailyRentalRate = movie.DailyRentalRate,
            Genre = new()
            {
                Id = movie.GenreId,
                Name = genreName,
            },
            Id = request.Id,
            NumberInStock = movie.NumberInStock,
            Rating = movie.Rating,
            Title = movie.Title,
        };
    }

    public override async Task<ListMoviesResponse> ListMovies(ListMoviesRequest request, ServerCallContext context)
    {
        List<MoviePreview> movies = await _movieQueries
           .GetMoviesAsync();

        return new()
        {
            Movies =
             {
                 movies.Select(movie => new Movie()
                 {
                     Title = movie.Title,
                     Id = movie.Id,
                     DailyRentalRate = movie.DailyRentalRate,
                     Genre = new ()
                     {
                         Name = movie.GenreName,
                         Id = movie.GenreId,
                     },
                     NumberInStock = movie.NumberInStock,
                     Rating = movie.Rating,
                 }),
             },
        };
    }

    public override async Task<ListExtendedMoviesResponse> ListExtendedMovies(ListMoviesRequest request, ServerCallContext context)
    {
        if (request.UserId is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "User id not found"));
        }

        List<MovieExtendedPreview> movies = await _movieQueries
            .GetMoviesExtendedAsync(request.UserId.Value);

        return new()
        {
            Movies =
            {
                movies.Select(movie => new MovieExtended()
                {
                    Id = movie.Id,
                    DailyRentalRate = movie.DailyRentalRate,
                    Genre = new ()
                    {
                        Name = movie.GenreName,
                        Id = movie.GenreId,
                    },
                    NumberInStock = movie.NumberInStock,
                    Liked = movie.Liked,
                    Title = movie.Title,
                    Rating = movie.Rating,
                    UpdatedDate = movie.UpdatedDate.ToUniversalTime().ToTimestamp(),
                }),
            },
        };
    }

    public override async Task<Movie> GetMovie(GetMovieRequest request, ServerCallContext context)
    {
        MoviePreview? movie = await _movieQueries.GetMovieAsync(request.Id);

        if (movie is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Movie not found"));
        }

        return new()
        {
            Id = movie.Id,
            Rating = movie.Rating,
            DailyRentalRate = movie.DailyRentalRate,
            Genre = new()
            {
                Id = movie.GenreId,
                Name = movie.GenreName,
            },
            NumberInStock = movie.NumberInStock,
            Title = movie.Title,
        };
    }

    public override async Task<MovieExtended> GetMovieExtended(GetMovieExtendedRequest request, ServerCallContext context)
    {
        MovieExtendedPreview? movie = await _movieQueries.GetMovieExtendedAsync(request.Id, request.UserId);

        if (movie is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Movie not found"));
        }

        return new()
        {
            Id = movie.Id,
            Rating = movie.Rating,
            UpdatedDate = movie.UpdatedDate.ToUniversalTime().ToTimestamp(),
            DailyRentalRate = movie.DailyRentalRate,
            Genre = new()
            {
                Id = movie.GenreId,
                Name = movie.GenreName,
            },
            NumberInStock = movie.NumberInStock,
            Liked = movie.Liked,
            Title = movie.Title,
        };
    }

    #endregion Movies
}
