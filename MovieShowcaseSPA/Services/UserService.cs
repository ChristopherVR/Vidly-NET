using System.Security.Claims;

namespace MoveShowcaseDDD.Services;
public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public string GetName() =>
        _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName) ?? throw new ArgumentNullException("Name cannot be null");

    public string GetSurname()
        => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Surname) ?? throw new ArgumentNullException("Surname cannot be null");

    public int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User Id cannot be null"));

    public string GetUserName() =>
        _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? throw new ArgumentNullException("UserName cannot be null");
}

