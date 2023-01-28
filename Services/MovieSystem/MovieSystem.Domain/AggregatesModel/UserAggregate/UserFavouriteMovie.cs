using System;
using MovieSystem.Domain.Exceptions;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.UserAggregate;

public class UserFavouriteMovie : Entity
{
    public string UpdatedUser { get; private set; }
    public DateTime UpdatedDate { get; private set; } = DateTime.Now;
    public int MovieId { get; private set; }
    public bool Liked { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserFavouriteMovie()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }

    public UserFavouriteMovie(
        int movieId,
        bool liked,
        string user)
    {
        MovieId = movieId;
        UpdatedUser = user;
        Liked = liked;
    }

    public void ToggleFavourite(bool liked, string user)
    {
        if (liked == Liked)
        {
            throw new MovieDomainException($"Movie is already {(liked ? "liked" : "disliked")}.");
        }
        Liked = liked;
        UpdatedUser = user;
    }
}
