using System;
using System.Collections.Generic;
using System.Linq;
using MovieSystem.Domain.Events;
using MovieSystem.Domain.Exceptions;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.UserAggregate;
public class User : Entity, IAggregateRoot
{
    public string UpdatedUser { get; private set; }
    public DateTime UpdatedDate { get; private set; } = DateTime.Now;
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Username { get; private set; }
    public string HashedPassword { get; private set; }
    public UserDetails UserDetails { get; private set; } = null!;
    // DDD Patterns comment
    // Using a private collection field, better for DDD Aggregate's encapsulation
    // so UserFavouriteMovie cannot be added from "outside the AggregateRoot" directly to the collection,
    // but only through the method UserAggregate.UserFavouriteMovie() which includes behaviour.
    private readonly List<UserFavouriteMovie> _userFavouriteMovies = new();
    public IReadOnlyCollection<UserFavouriteMovie> UserFavouriteMovies => _userFavouriteMovies;

    private User()
    {
        Id = 1;
        UpdatedUser = "initial";
        UpdatedDate = DateTime.MinValue;
        Name = "Christopher";
        Surname = "van Rooyen";
        Username = "ChristopherVR";
        HashedPassword = string.Empty;
    }

    public User(
        string name,
        string username,
        string surname,
        string hashedPassword,
        string phoneNumber,
        string address,
        string homeNumber,
        string? imageUrl
        )
    {
        ValidateDetails(name, surname, username, address, phoneNumber, homeNumber, imageUrl);
        Name = name;
        Username = username;
        Surname = surname;
        HashedPassword = hashedPassword;
        UserDetails = new(address, phoneNumber, homeNumber, imageUrl);
        UpdatedUser = username;

        // Add the UserCreatedDomainEvent to the domain events collection
        // to be raised/dispatched when committing changes to the database
        UserCreatedDomainEvent userCreatedDomainEvent = new(this);
        AddDomainEvent(userCreatedDomainEvent);
    }

    internal static void ValidateDetails(string name, string surname, string username, string address, string phoneNumber, string homeNumber, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new MovieDomainException("Surname cannot be null");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new MovieDomainException("Name cannot be null");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new MovieDomainException("Username cannot be null");
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new MovieDomainException("Address cannot be null");
        }

        if (string.IsNullOrWhiteSpace(phoneNumber) || !new System.ComponentModel.DataAnnotations.PhoneAttribute().IsValid(phoneNumber))
        {
            throw new MovieDomainException("Invalid phone number");
        }

        if (string.IsNullOrWhiteSpace(homeNumber) || !new System.ComponentModel.DataAnnotations.PhoneAttribute().IsValid(homeNumber))
        {
            throw new MovieDomainException("Invalid home number");
        }

        if (!string.IsNullOrEmpty(imageUrl) && !new System.ComponentModel.DataAnnotations.UrlAttribute().IsValid(imageUrl))
        {
            throw new MovieDomainException("Image url is not a valid URI");
        }
    }


    public void ToggleFavourite(int movieId, bool liked, string user)
    {
        UserFavouriteMovie? favMovie = _userFavouriteMovies
            .FirstOrDefault(x => x.MovieId == movieId);

        if (favMovie is null)
        {
            favMovie = new(movieId, liked, user);
            _userFavouriteMovies.Add(favMovie);
        }
        else
        {
            favMovie.ToggleFavourite(liked, user);
        }
    }

    public void UpdatePassword(string user, string password)
    {
        UpdatedUser = user;
        HashedPassword = password;
    }

    public void Update(string user, string name, string surname, string phoneNumber, string address, string homeNumber, string? imageUrl)
    {
        ValidateDetails(name, surname, Username, address, phoneNumber, homeNumber, imageUrl);
        Name = name;
        Surname = surname;
        UserDetails = new(address, phoneNumber, homeNumber, imageUrl);
        UpdatedUser = user;
    }

    public static User CreateInitialSeedData() => new();

    public void Update(string user, string name, string surname, string phoneNumber, string address, string homeNumber, Uri imageUrl)
    {
        ValidateDetails(name, surname, Username, address, phoneNumber, homeNumber, imageUrl.ToString());
        Name = name;
        Surname = surname;
        UserDetails = new(address, phoneNumber, homeNumber, imageUrl.ToString());
        UpdatedUser = user;
    }
}
