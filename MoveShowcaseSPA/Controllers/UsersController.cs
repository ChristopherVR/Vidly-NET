using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Services;
using System.Security.Claims;
using static UserSystem.V1.Users;

namespace MoveShowcaseDDD.Areas.Controllers;
[ApiConventionType(typeof(DefaultApiConventions))]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersClient _usersClient;
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public UsersController(UsersClient usersClient, IUserService userService, ITokenService tokenService, ILogger<UsersController> logger)
    {
        _usersClient = usersClient ?? throw new ArgumentNullException(nameof(usersClient));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private async Task<string> LoginUser(string username, string id, string name, string surname)
    {
        string token = _tokenService.BuildToken(new(id, username, name, surname, "Admin"));

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
        var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
        var principal = new ClaimsPrincipal(identity);

        //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
        {
            AllowRefresh = true,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
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

        return token;
    }

    [HttpGet("user")]
    [Authorize]
    public new IActionResult User()
    {
        return Ok(new
        {
            Id = _userService.GetUserId(),
            Username = _userService.GetUserName(),
            Name = _userService.GetName(),
            Surname = _userService.GetSurname(),
        });
    }


    public record UserLoginPost(string Username, string Password);
    [HttpPost]
    [Route("user/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Login(
    [FromServices] IConfiguration config,
    [FromServices] IWebHostEnvironment env,
    [FromBody] UserLoginPost data)
    {
        if (config.GetValue<bool>("BypassAuthentication") && env.IsDevelopment())
        {
            return Ok(await LoginUser(data.Username, "1", "TestUser", "Mock"));
        }

        // TODO: Get password and validate user
        try
        {
            var userDetails = await _usersClient.GetUserExtendedAsync(new()
            {
                Username = data.Username,
            });

            return Ok(await LoginUser(data.Username, userDetails.Id.ToString(), userDetails.Name, userDetails.Surname));
        }
        catch(RpcException ex) when (ex.StatusCode == global::Grpc.Core.StatusCode.NotFound)
        {
            return NotFound();
        }
        catch(RpcException ex)
        {
            _logger.LogError("Error trying to retrieve user detials", ex);
            return BadRequest(); // TODO: return internal server error
        }

    }

    [HttpPost]
    [Authorize]
    [Route("user/logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }

    public record UserPreview(string Username, string Name, string Surname, string HomeNumber, string PhoneNumber, string Address, string? ImageUrl);
    [HttpPost]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
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

    public record UserPreviewPatch(string Name, string Surname, string HomeNumber, string PhoneNumber, string Address, string? ImageUrl);
    [HttpPatch]
    [Authorize]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateUser([FromBody] UserPreviewPatch data)
    {
        await _usersClient.UpdateUserAsync(new()
        {
            Id = _userService.GetUserId(),
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

