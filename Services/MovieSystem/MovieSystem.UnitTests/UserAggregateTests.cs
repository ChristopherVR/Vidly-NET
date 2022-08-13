using MovieSystem.Domain.AggregatesModel.UserAggregate;
using MovieSystem.Domain.Exceptions;
using System;
using Xunit;

namespace MovieSystem.UnitTests.Domain;
public class UserAggregateTests
{
    [Fact]
    public void Create_user_success()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "this is my surnmae";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "1234";
        string address = "12th Avenue";
        string homeNumber = "1234";
        string? imageUrl = default;
        // Act
        var user = new User(name, username, surname, hashedPassword, phoneNumber, address, homeNumber, imageUrl);

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    public void Create_user_success_update_details_success()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "this is my surnmae";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "1234";
        string address = "12th Avenue";
        string homeNumber = "1234";
        string? imageUrl = default;

        string updatedName = "this is the updated name";
        string updatedSurname = "this is the updated surname";
        string updatedPhoneNumber = "554326";
        string updatedHomeNumber = "4321";
        string updatedHomeAddress = "12th Avenue";
        string updatedImageUrl = "https://www.google.co.za";

        // Act
        User user = new(name, username, surname, hashedPassword, phoneNumber, address, homeNumber, imageUrl);
#pragma warning disable CA2234 // Pass system uri objects instead of strings
        user.Update("unit_tests", updatedName, updatedSurname, updatedPhoneNumber, updatedHomeAddress, updatedHomeNumber, updatedImageUrl);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

        // Assert
        Assert.NotNull(user);
        Assert.Equal(updatedName, user.Name);
        Assert.Equal(updatedSurname, user.Surname);
        Assert.Equal(updatedPhoneNumber, user.UserDetails.PersonalNumber);
        Assert.Equal(updatedHomeNumber, user.UserDetails.HomeNumber);
        Assert.Equal(updatedHomeAddress, user.UserDetails.Address);
        Assert.Equal(updatedImageUrl, user.UserDetails.ImageUrl);
    }

    [Fact]
    public void Create_user_phone_number_invalid_exception()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "this is my surnmae";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "SDASD";
        string address = "12th Avenue";
        string homeNumber = "0795072154";
        string? imageUrl = default;
        var expectedErrorMessage = "Invalid phone number";
        // Act
        Exception exception = Record
            .Exception(() => new User(
                name,
                username,
                surname,
                hashedPassword,
                phoneNumber,
                address,
                homeNumber,
                imageUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_user_name_null_invalid_exception()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "this is my surnmae";
        string name = "";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "0795072154";
        string address = "12th Avenue";
        string homeNumber = "0795072154";
        string? imageUrl = default;
        var expectedErrorMessage = "Name cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new User(
                name,
                username,
                surname,
                hashedPassword,
                phoneNumber,
                address,
                homeNumber,
                imageUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_user_surname_null_invalid_exception()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "0795072154";
        string address = "12th Avenue";
        string homeNumber = "0795072154";
        string? imageUrl = default;
        var expectedErrorMessage = "Surname cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new User(
                name,
                username,
                surname,
                hashedPassword,
                phoneNumber,
                address,
                homeNumber,
                imageUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_user_username_null_invalid_exception()
    {
        // Arrange 
        string username = "";
        string surname = "this is my surnmae";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "0795072154";
        string address = "12th Avenue";
        string homeNumber = "0795072154";
        string? imageUrl = default;
        var expectedErrorMessage = "Username cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new User(
                name,
                username,
                surname,
                hashedPassword,
                phoneNumber,
                address,
                homeNumber,
                imageUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_user_address_null_invalid_exception()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "this is my surnmae";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "0795072154";
        string address = "";
        string homeNumber = "0795072154";
        string? imageUrl = default;
        var expectedErrorMessage = "Address cannot be null";
        // Act
        Exception exception = Record
            .Exception(() => new User(
                name,
                username,
                surname,
                hashedPassword,
                phoneNumber,
                address,
                homeNumber,
                imageUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public void Create_user_invalid_image_url_exception()
    {
        // Arrange 
        string username = "this is a username";
        string surname = "this is my surnmae";
        string name = "what a cool name";
        string hashedPassword = "ASDASDASD";
        string phoneNumber = "0795072154";
        string address = "12th Avenue";
        string homeNumber = "0795072154";
        string? imageUrl = "1234";
        var expectedErrorMessage = "Image url is not a valid URI";
        // Act
        Exception exception = Record
            .Exception(() => new User(
                name,
                username,
                surname,
                hashedPassword,
                phoneNumber,
                address,
                homeNumber,
                imageUrl));

        // Assert
        Assert.IsType<MovieDomainException>(exception);
        Assert.Equal(expectedErrorMessage, exception.Message);
    }
}

