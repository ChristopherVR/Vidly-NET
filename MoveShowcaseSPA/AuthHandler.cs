using System.Net.Http.Headers;

namespace MoveShowcaseDDD;
public class AuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        // TODO: Handle cookie auth here
        return await base.SendAsync(request, cancellationToken);
    }
}
