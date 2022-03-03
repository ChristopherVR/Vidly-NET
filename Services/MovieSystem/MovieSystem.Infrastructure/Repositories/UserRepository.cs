using Microsoft.EntityFrameworkCore;
using MovieSystem.Domain.AggregatesModel.UserAggregate;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly MovieContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public UserRepository(MovieContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<User?> GetAsync(int entityId)
    {
        User? entity = await _context
            .Users
            .FirstOrDefaultAsync(u => u.Id == entityId);

        if (entity == null)
        {
            entity = _context
                .Users
                .Local
                .FirstOrDefault(u => u.Id == entityId);
        }

        if (entity is not null)
        {
            await _context.Entry(entity)
                .Collection(r => r.UserFavouriteMovies)
                .LoadAsync();
        }

        return entity;
    }

    public User Add(User entity) => _context.Users.Add(entity).Entity;

    public void Update(User entity) => _context.Entry(entity).State = EntityState.Modified;

    public async Task<User?> GetAsync(string username)
    {
        User? entity = await _context
            .Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (entity == null)
        {
            entity = _context
                .Users
                .Local
                .FirstOrDefault(u => u.Username == username);
        }

        if (entity is not null)
        {
            await _context.Entry(entity)
                .Collection(r => r.UserFavouriteMovies)
                .LoadAsync();
        }

        return entity;
    }
}
