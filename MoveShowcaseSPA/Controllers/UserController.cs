using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static UserSystem.V1.Users;

namespace MoveShowcaseDDD.Areas.Controllers;
// TODO: fix endpoint routes
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UsersClient _usersClient;
    private readonly ILogger<UserController> _logger;
    public UserController(UsersClient usersClient, ILogger<UserController> logger)
    {
        _usersClient = usersClient ?? throw new ArgumentNullException(nameof(usersClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private async Task LoginUser(string username, string id, string name, string surname)
    {
        //A claim is a statement about a subject by an issuer and    
        //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Surname, surname),
            new Claim(ClaimTypes.GivenName, username),
        };

        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
        var principal = new ClaimsPrincipal(identity);

        //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
        {
            AllowRefresh = true,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        });
    }

    [HttpGet]
    [Authorize]
    public new IActionResult User()
    {
        return Ok();
        // return Ok(new
        // {
        //     Id = _userService.GetUserId(),
        //     Username = _userService.GetUsername(),
        //     Name = _userService.GetName(),
        //     Surname = _userService.GetSurname(),
        // });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(
    [FromServices] IConfiguration config,
    [FromServices] IWebHostEnvironment env,
    string username,
    string password)
    {
        _ = password;

        if (config.GetValue<bool>("BypassAuthentication") && env.IsDevelopment())
        {
            await LoginUser(username, "1", "TestUser", "Mock");
        }
        else
        {
            // TODO: Get password and validate user
            var userDetails = await _usersClient.GetUserExtendedAsync(new()
            {
                Username = username,
            });
            await LoginUser(username, userDetails.Id.ToString(), userDetails.Name, userDetails.Surname);
        }

        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }

    public record UserPreview(string Username, string Name, string Surname, string HomeNumber, string PhoneNumber, string Address, string? ImageUrl);
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserPreview data)
    {
        var response = await _usersClient.CreateUserAsync(new()
        {
            Address = data.Address,
            HomeNumber = data.HomeNumber,
            PhoneNumber = data.PhoneNumber,
            Name = data.Name,
            Surname = data.Surname,
            ImageUrl = data.ImageUrl,
            Username = data.Username,
        });

        return Ok(response.Id);
    }

    public record UserPreviewPatch(int Id, string Name, string Surname, string HomeNumber, string PhoneNumber, string Address, string? ImageUrl);
    [HttpPatch]
    [Authorize]
    [Route("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserPreviewPatch data)
    {
        await _usersClient.UpdateUserAsync(new()
        {
            Id = data.Id,
            Surname = data.Surname,
            Name = data.Name,
            Address = data.Address,
            PhoneNumber = data.PhoneNumber,
            HomeNumber = data.HomeNumber,
            ImageUrl = data.ImageUrl,
        });

        return NoContent();
    }


}

