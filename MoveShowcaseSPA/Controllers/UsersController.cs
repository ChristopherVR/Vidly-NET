using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        _userService = tokenService ?? throw new ArgumentNullException(nameof(userService));
        _tokenService = userService ?? throw new ArgumentNullException(nameof(tokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private async Task LoginUser(string username, string id, string name, string surname)
    {
        string token = _tokenService.BuildToken(new(id, username, name, surname, "Admin"));

       
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
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Login(
    [FromServices] IConfiguration config,
    [FromServices] IWebHostEnvironment env,
    [FromBody] UserLoginPost data)
    {
        if (config.GetValue<bool>("BypassAuthentication") && env.IsDevelopment())
        {
            await LoginUser(data.Username, "1", "TestUser", "Mock");
        }
        else
        {
            // TODO: Get password and validate user
            var userDetails = await _usersClient.GetUserExtendedAsync(new()
            {
                Username = data.Username,
            });
            await LoginUser(data.Username, userDetails.Id.ToString(), userDetails.Name, userDetails.Surname);
        }

        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("logout")]
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

