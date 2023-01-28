using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MovieShowcaseSPA.Services;
using System.Net.Http.Headers;


namespace MovieShowcaseSPA.Infrastructure.Authorization;
public class AuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public AuthHandler(
        IHttpContextAccessor httpContextAccessor,
        ITokenService tokenService,
        IConfiguration config,
        IWebHostEnvironment env)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _configuration = config ?? throw new ArgumentNullException(nameof(config));
        _env = env ?? throw new ArgumentNullException(nameof(env));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        string? token = _httpContextAccessor.HttpContext.Items.TryGetValue("Authorization", out var tokenValue) && tokenValue is string value
            ? value
            : await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync(cancellationToken: cancellationToken);

        if (token is null && _configuration.GetValue<bool>(AppOptions.EnableDemoMode) && _env.IsDevelopment())
        {
            token = _tokenService.BuildToken(new(Id: 1, Username: "Test_User", Name: "Test", Surname: "Test"));
        }

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
