namespace People.Domain.Common;

public class BaseAuditableEntity<TId> : BaseEntity<TId> 
    where TId : struct, IEquatable<TId>, IComparable<TId>
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
