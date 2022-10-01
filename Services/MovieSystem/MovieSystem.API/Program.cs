
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieSystem.API.Grpc;
using MovieSystem.API.Infrastructure.Authorization;
using MovieSystem.API.Infrastructure.AutofacModules;
using MovieSystem.API.Services;
using System.Text;
#pragma warning disable CA1852
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
    builder.Services.AddGrpcReflection();
}

builder.Services.AddGrpc();

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Movie"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        }));

AuthOptions authOptions = builder.Configuration.GetSection(AuthOptions.Name).Get<AuthOptions>()!;

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
        ValidIssuer = authOptions.Authority,
        ValidAudience = authOptions.Authority,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, UserHasSameIdAccessAuthorizationHandler>();

// Register services directly with Autofac here.
// Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new QueryModule(builder.Configuration.GetConnectionString("Movie")!));
    containerBuilder.RegisterModule(new RepositoryModule());
    containerBuilder.RegisterModule(new MediatorModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<MoviesServiceV1>();
app.MapGrpcService<UsersServiceV1>();
app.MapGrpcService<GenresServiceV1>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.UseStaticFiles();

app.Run();
#pragma warning restore CA1852
