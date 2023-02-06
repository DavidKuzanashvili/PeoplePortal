namespace People.Application.Common;

public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken);
}
