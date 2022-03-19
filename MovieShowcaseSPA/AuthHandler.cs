using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;


namespace MoveShowcaseDDD;
public class AuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthHandler(IHttpContextAccessor httpContextAccessor) 
        => (_httpContextAccessor) = (httpContextAccessor);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        string? token = await _httpContextAccessor.HttpContext.GetUserAccessTokenAsync(cancellationToken: cancellationToken);

        if (token is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

        return await base.SendAsync(request, cancellationToken);
    }
}
