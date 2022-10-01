using MovieSystem.Domain.SeedWork;
using MovieSystem.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using MovieSystem.Domain.AggregatesModel.UserAggregate;
using MovieSystem.Domain.AggregatesModel.GenreAggregate;
using System.Threading.Tasks;
using System.Threading;

namespace MovieSystem.Infrastructure;

public class MovieContext : DbContext, IUnitOfWork
{
    public const string DEFAULTSCHEMA = "Movie";

    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;

    private readonly IMediator _mediator;

    public MovieContext(DbContextOptions<MovieContext> options, IMediator mediator) : base(options) =>
        _mediator = mediator;

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}

