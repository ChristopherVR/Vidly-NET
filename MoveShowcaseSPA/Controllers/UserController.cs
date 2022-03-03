using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveShowcaseDDD.Pages;
using System.Security.Claims;
using static UserSystem.V1.Users;

namespace MoveShowcaseDDD.Areas.Controllers;
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

    public record UserPreview(int Id, string Username, string HashedPassword, string Name, string Surname);
    [HttpPost]
    public async Task<IActionResult> Login([FromServices] IConfiguration config, [FromServices] IWebHostEnvironment env, string username, string password)
    {
        // TODO: Hash password compare with user in DB and log user in.
        _ = username;
        _ = password;

        if (config.GetValue<bool>("BypassAuthentication") && env.IsDevelopment())
        {
            //A claim is a statement about a subject by an issuer and    
            //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Peter"),
                new Claim(ClaimTypes.Surname, "Potter"),
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

        return RedirectToPage(nameof(IndexModel));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage(nameof(IndexModel));
    }
}

