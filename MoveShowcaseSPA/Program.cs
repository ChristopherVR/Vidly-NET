using Microsoft.AspNetCore.Authentication.Cookies;
using MoveShowcaseDDD;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => x.LoginPath="/user/login");

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

// builder.Services.AddGrpcClient<UserSystem.V1.Users.UsersClient>(o =>
// {
//     o.Address = new Uri(movieSystemUrl);
// }).AddHttpMessageHandler<AuthHandler>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
