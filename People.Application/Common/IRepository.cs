using People.Domain.Common;

namespace People.Application.Common;

public interface IRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : struct, IEquatable<TId>, IComparable<TId>
{
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetAsync(TId id, CancellationToken cancellationToken);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
