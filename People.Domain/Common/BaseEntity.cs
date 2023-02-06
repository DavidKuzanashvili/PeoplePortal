namespace People.Domain.Common;

public class BaseEntity<TId> : IEntity<TId>
    where TId : struct, IEquatable<TId>, IComparable<TId>
{
    public virtual TId EntityId { get; protected set; }
}
