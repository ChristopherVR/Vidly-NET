using Microsoft.AspNetCore.Http;
using Moq;
using MovieShowcaseSPA.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace MovieShowcaseWeb.Tests;

public class UserServiceTests
{
    [Fact]
    public void Get_user_id_success()
    {
        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "Peter"),
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expected = 1;

        Assert.Equal(expected, userService.GetUserId());
    }

    [Fact]
    public void Get_user_id_throw_exception_null()
    {

        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Peter"),
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expectedMessage = "Value cannot be null. (Parameter 'User Id cannot be null')";

        var exception = Record.Exception(() => userService.GetUserId());

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void Get_user_name_success()
    {
        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.GivenName, "Peter"),
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expected = "Peter";

        Assert.Equal(expected, userService.GetName());
    }

    [Fact]
    public void Get_user_name_throw_exception_null()
    {

        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expectedMessage = "Value cannot be null. (Parameter 'Name cannot be null')";

        var exception = Record.Exception(() => userService.GetName());

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal(expectedMessage, exception.Message);
    }


    [Fact]
    public void Get_username_success()
    {
        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Peter"),
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expected = "Peter";

        Assert.Equal(expected, userService.GetUserName());
    }

    [Fact]
    public void Get_user_username_throw_exception_null()
    {

        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expectedMessage = "Value cannot be null. (Parameter 'UserName cannot be null')";

        var exception = Record.Exception(() => userService.GetUserName());

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void Get_surname_success()
    {
        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "test"),
            new Claim(ClaimTypes.Name, "Peter"),
            new Claim(ClaimTypes.Surname, "Potter"),
        });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expected = "Potter";

        Assert.Equal(expected, userService.GetSurname());
    }

    [Fact]
    public void Get_user_surname_throw_exception_null()
    {

        var mockContextAccessor = Mock.Of<IHttpContextAccessor>();
        var identity = new ClaimsIdentity(new List<Claim>() { });
        mockContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity),
        };

        var userService = new UserService(mockContextAccessor);

        var expectedMessage = "Value cannot be null. (Parameter 'Surname cannot be null')";

        var exception = Record.Exception(() => userService.GetSurname());

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal(expectedMessage, exception.Message);
    }
}
