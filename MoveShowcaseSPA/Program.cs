using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoveShowcaseDDD;
using MoveShowcaseDDD.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddAuthorization();

var servicesSection = builder.Configuration.GetSection("Services");

string movieSystemUrl = servicesSection.GetValue<string>("MovieSystem");
builder.Services.AddGrpcClient<MovieSystem.V1.Movies.MoviesClient>(o =>
{
    o.Address = new Uri(movieSystemUrl);
}).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddGrpcClient<UserSystem.V1.Users.UsersClient>(o =>
{
    o.Address = new Uri(movieSystemUrl);
}).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddGrpcClient<GenreSystem.V1.Genres.GenresClient>(o =>
{
    o.Address = new Uri(movieSystemUrl);
}).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Issuer"],
        ValidAudience = builder.Configuration["Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Key"])),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
