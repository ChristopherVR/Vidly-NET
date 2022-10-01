using Microsoft.IdentityModel.Tokens;
using MoveShowcaseDDD.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoveShowcaseDDD.Services;
public class TokenService : ITokenService
{
    private const double EXPIRY_DURATION_MINUTES = 30;

    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration config) => _configuration = config;

    public string BuildToken(User user)
    {
        Claim[]? claims = new[]
        {
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };
        string? key = _configuration.GetValue<string>("Key");
        string? issuer = _configuration.GetValue<string>("Issuer");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public async Task<bool> TokenValid(string token)
    {
        string? key = _configuration.GetValue<string>("Key");
        string? issuer = _configuration.GetValue<string>("Issuer");
        byte[]? mySecret = Encoding.UTF8.GetBytes(key!);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);

        TokenValidationResult? validatedToken = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = mySecurityKey,
        });
        return validatedToken.IsValid;
    }
}

