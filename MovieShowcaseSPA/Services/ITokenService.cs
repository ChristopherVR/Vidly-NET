using MovieShowcaseSPA.Models;

namespace MovieShowcaseSPA.Services;
public interface ITokenService
{
    string BuildToken(User user);
    Task<bool> TokenValid(string token);
}
