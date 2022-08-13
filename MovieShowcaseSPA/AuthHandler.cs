using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MoveShowcaseDDD.Services;
using System.Net.Http.Headers;


namespace MoveShowcaseDDD;
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

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request!!, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        string? token;

        if (_httpContextAccessor.HttpContext.Items.TryGetValue("Authorization", out var tokenValue) && tokenValue is string value)
        {
            token = value;
        }
        else
        {
            token = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync(cancellationToken: cancellationToken);
        }

        if (token is null && _configuration.GetValue<bool>("BypassAuthentication") && _env.IsDevelopment())
        {
            token = _tokenService.BuildToken(new("1", "Test_User", "Test", "Test", "Admin"));
        }

        if (token is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

        return await base.SendAsync(request, cancellationToken);
    }
}
