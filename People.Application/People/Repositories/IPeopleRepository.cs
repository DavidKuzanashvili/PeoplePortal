using People.Application.Common;
using People.Application.People.Dtos;
using People.Domain.Entities;

namespace People.Application.People.Repositories;

public interface IPeopleRepository : IRepository<Person, Guid>
{
    Task<Person?> GetAggregateAsync(Guid id, CancellationToken cancellationToken);

    Task<Person?> GetByIdReadonlyAsync(Guid id, CancellationToken cancellationToken);

    Task SetImagePathAsync(Guid id, string path, CancellationToken cancellationToken);

    Task<(List<Person>, int)> GetFilteredReadonlyAsync(
        SortingOrder sort,
        int skip,
        int take,
        CancellationToken cancellationToken);

    Task<List<Person>> GetByIdsReadonlyAsync(
        IEnumerable<Guid> ids, 
        CancellationToken cancellationToken);

    Task<List<Person>> GetWithRelatedPeopleReadOnlyAsyn(CancellationToken cancellationToken);
}
