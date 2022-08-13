using System.Threading.Tasks;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.UserAggregate;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetAsync(int entityId);
    public Task<User?> GetAsync(string username);
    public User Add(User entity);
    public void Update(User entity);
}
