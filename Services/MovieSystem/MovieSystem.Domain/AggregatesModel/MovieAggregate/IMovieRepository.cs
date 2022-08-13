using System.Threading.Tasks;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.MovieAggregate;
public interface IMovieRepository : IRepository<Movie>
{
    Task<Movie?> GetAsync(int id);
    Movie Add(Movie entity);
    void Update(Movie entity);
    void Remove(Movie entity);
}
