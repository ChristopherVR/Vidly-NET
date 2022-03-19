using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Services;
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

    private object? LoginUser(string username, string id, string name, string surname)
    {
        string token = _tokenService.BuildToken(new(id, username, name, surname, "Admin"));
        return new JsonResult(new
        {
            Token = token,
            Name = name,
            Surname = surname,
            Username = username,
            Id = id,
        }).Value;
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
    [AllowAnonymous]
    public async Task<IActionResult> Login(
    [FromServices] IConfiguration config,
    [FromServices] IWebHostEnvironment env,
    [FromBody] UserLoginPost data)
    {
        if (config.GetValue<bool>("BypassAuthentication") && env.IsDevelopment())
        {
            return Ok(LoginUser(data.Username, "1", "TestUser", "Mock"));
        }

        try
        {
            var userDetails = await _usersClient.GetUserExtendedAsync(new()
            {
                Username = data.Username,
            });

            var hasher = new PasswordHasher<IdentityUser>();
            IdentityUser identityUser = new (userDetails.Id.ToString());

            if (PasswordVerificationResult.Failed == hasher.VerifyHashedPassword(identityUser, userDetails.HashedPassword, data.Password))
            {
                return BadRequest();
            }

            return Ok(LoginUser(data.Username, userDetails.Id.ToString(), userDetails.Name, userDetails.Surname));
        }
        catch(RpcException ex) when (ex.StatusCode == global::Grpc.Core.StatusCode.NotFound)
        {
            return BadRequest(); // Do not indicate to the end-user that the user does not exist.
        }
        catch(RpcException ex)
        {
            _logger.LogError("Error trying to retrieve user detials", ex);
            return BadRequest();
        }

    }

    public record UserDTO(
        string Username, 
        string Password,
        string Name, 
        string Surname,
        string HomeNumber, 
        string PhoneNumber,
        string Address, 
        string? ImageUrl);
    [HttpPost]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO data)
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

        var hasher = new PasswordHasher<IdentityUser>();
        IdentityUser identityUser = new(response.Id.ToString());

        string token = _tokenService.BuildToken(new(response.Id.ToString(), response.Username, response.Name, response.Surname, "Admin"));
        HttpContext.Items.Add("Authorization", $"Bearer {token}");
        await _usersClient.CreateUserHashedPasswordAsync(new()
        {
            HashedPassword = hasher.HashPassword(identityUser, data.Password),
            UserId = response.Id,
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

