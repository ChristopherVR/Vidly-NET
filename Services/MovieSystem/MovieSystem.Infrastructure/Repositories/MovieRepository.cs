using Microsoft.EntityFrameworkCore;
using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Infrastructure.Repositories;
public class MovieRepository : IMovieRepository
{
    private readonly MovieContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public MovieRepository(MovieContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Movie?> GetAsync(int entityId)
    {
        Movie? entity = await _context
            .Movies
            .FirstOrDefaultAsync(u => u.Id == entityId);

        if (entity == null)
        {
            entity = _context
                .Movies
                .Local
                .FirstOrDefault(u => u.Id == entityId);
        }

        return entity;
    }

    public Movie Add(Movie entity) => _context.Movies.Add(entity).Entity;

    public void Update(Movie entity) => _context.Entry(entity).State = EntityState.Modified;
    public void Remove(Movie entity) => _context.Entry(entity).State = EntityState.Deleted;
}
