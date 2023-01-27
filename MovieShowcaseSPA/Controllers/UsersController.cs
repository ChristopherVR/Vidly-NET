using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieShowcaseSPA.Models;
using MovieShowcaseSPA.Services;
using static UserSystem.V1.Users;

namespace MovieShowcaseSPA.Controllers;
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
        _usersClient = usersClient;
        _userService = userService;
        _tokenService = tokenService;
        _logger = logger;
    }

    private LoginUser GetLoginObject(string username, int id, string name, string surname)
    {
        string token = _tokenService.BuildToken(new(id, username, name, surname));
        return new(token, name, surname, username, id);
    }

    [HttpGet("user")]
    [Authorize]
    public new IActionResult User() => Ok(new
    {
        Id = _userService.GetUserId(),
        Username = _userService.GetUserName(),
        Name = _userService.GetName(),
        Surname = _userService.GetSurname(),
    });


    [HttpPost]
    [Route("user/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
    [FromServices] IConfiguration config,
    [FromServices] IWebHostEnvironment env,
    [FromBody] UserLoginPost data)
    {
        if (config.GetValue<bool>(AppOptions.EnableDemoMode) && env.IsDevelopment())
        {
            return Ok(GetLoginObject(data.Username, id: 1, name: "TestUser", surname: "Mock"));
        }

        try
        {
            UserSystem.V1.UserExtended? userDetails = await _usersClient.GetUserExtendedAsync(new()
            {
                Username = data.Username,
            });

            var hasher = new PasswordHasher<IdentityUser>();
            IdentityUser identityUser = new(userDetails.Id.ToString());

            if (PasswordVerificationResult.Failed == hasher.VerifyHashedPassword(identityUser, userDetails.HashedPassword, data.Password))
            {
                return BadRequest();
            }

            return Ok(GetLoginObject(data.Username, userDetails.Id, userDetails.Name, userDetails.Surname));
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            return BadRequest(); // Do not indicate to the end-user that the user does not exist.
        }
        catch (RpcException ex)
        {
            _logger.LogError("Error trying to retrieve user detials", ex);
            return BadRequest();
        }

    }


    [HttpPost]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO data)
    {
        UserSystem.V1.UserExtended? response = await _usersClient.CreateUserAsync(new()
        {
            Address = data.Address,
            HomeNumber = data.HomeNumber,
            PhoneNumber = data.PhoneNumber,
            Name = data.Name,
            Surname = data.Surname,
            ImageUrl = data.ImageUrl?.ToString(),
            Username = data.Username,
        });

        var hasher = new PasswordHasher<IdentityUser>();
        IdentityUser identityUser = new(response.Id.ToString());

        string token = _tokenService.BuildToken(new(response.Id, response.Username, response.Name, response.Surname));

        HttpContext.Items.Add("Authorization", $"Bearer {token}");

        await _usersClient.CreateUserHashedPasswordAsync(new()
        {
            HashedPassword = hasher.HashPassword(identityUser, data.Password),
            UserId = response.Id,
        });

        return Ok(response.Id);
    }

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
            ImageUrl = data.ImageUrl?.ToString(),
        });

        return NoContent();
    }
}

