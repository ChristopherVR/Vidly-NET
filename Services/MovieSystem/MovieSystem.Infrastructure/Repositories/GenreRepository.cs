using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieSystem.Domain.AggregatesModel.GenreAggregate;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Infrastructure.Repositories;
public class GenreRepository : IGenreRepository
{
    private readonly MovieContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public GenreRepository(MovieContext context!!) =>
        _context = context;

    public async Task<Genre?> GetAsync(int id)
    {
        Genre? entity = await _context
            .Genres
            .FirstOrDefaultAsync(u => u.Id == id);

        if (entity == null)
        {
            entity = _context
                .Genres
                .Local
                .FirstOrDefault(u => u.Id == id);
        }

        return entity;
    }

    public Genre Add(Genre entity) => _context.Genres.Add(entity).Entity;

    public void Update(Genre entity) => _context.Entry(entity).State = EntityState.Modified;

    public void Remove(Genre entity) => _context.Remove(entity).State = EntityState.Deleted;
}
