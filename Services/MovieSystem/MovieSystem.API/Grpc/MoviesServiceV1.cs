using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MovieSystem.API.Application.Queries;
using MovieSystem.V1;

namespace MovieSystem.API.Grpc;
 [Authorize]
 public class MoviesServiceV1 : MovieSystem.V1.Movies.MoviesBase
 {
     private readonly ILogger<MoviesServiceV1> _logger;
     private readonly IMediator _mediator;
     private readonly IMovieQueries _movieQueries;
 
     public MoviesServiceV1(ILogger<MoviesServiceV1> logger, IMediator mediator, IMovieQueries movieQueries)
     {
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
         _movieQueries = movieQueries ?? throw new ArgumentNullException(nameof(movieQueries));
     }
 
     #region Movies
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
                     Name = movie.Name,
                     Description = movie.Description,
                     Id = movie.Id,
                     ImdbUrl  = movie.ImdbUrl,
                 })
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
                     Description = movie.Description,
                     Id = movie.Id,
                     ImdbUrl = movie.ImdbUrl,
                     Name = movie.Name,
                     Rating = ((int?) movie.Rating) ?? 0,
                     Reason = movie.Reason,
                     UpdatedDate = Timestamp.FromDateTime(movie.UpdatedDate),
                 })
             }
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
             Description = movie.Description,
             Id = movie.Id,
             ImdbUrl = movie.ImdbUrl,
             Name = movie.Name,
         };
     }
 
     public async override Task<MovieExtended> GetMovieExtended(GetMovieExtendedRequest request, ServerCallContext context)
     {
         MovieExtendedPreview? movie = await _movieQueries.GetMovieExtendedAsync(request.Id, request.UserId);
 
         if (movie is null)
         {
             throw new RpcException(new Status(StatusCode.NotFound, "Movie not found"));
         }
 
         return new()
         {
             Description = movie.Description,
             Id = movie.Id,
             ImdbUrl = movie.ImdbUrl,
             Name = movie.Name,
             Rating = ((int?)movie.Rating) ?? 0,
             Reason = movie.Reason,
             UpdatedDate = Timestamp.FromDateTime(movie.UpdatedDate),
         };
     }
 
     #endregion Movies
 }

