namespace MovieSystem.API.Infrastructure.Options;

public class AuthOptions
{
    public const string Name = "Auth";

    public string Authority { get; set; } = "http://localhost";
}

