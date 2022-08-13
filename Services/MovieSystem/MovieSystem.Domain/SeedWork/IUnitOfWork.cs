using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieSystem.Domain.SeedWork;
public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
