using System.Threading.Tasks;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.GenreAggregate;
public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre?> GetAsync(int id);
    Genre Add(Genre entity);
    void Update(Genre entity);
    void Remove(Genre entity);
}
