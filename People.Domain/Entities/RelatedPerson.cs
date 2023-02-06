using People.Domain.Helpers;

namespace People.Domain.Entities;

public class RelatedPerson
{
    private RelatedPerson() { }

    private RelatedPerson(Guid relatedPersonEntityId, RelatedPersonType relationType)
    {
        RelatedPersonEntityId = relatedPersonEntityId;
        RelationType = relationType;
    }

    public Guid RelatedPersonEntityId { get; private set; }
    public RelatedPersonType RelationType { get; private set; }

    public static RelatedPerson Create(
        Guid relatedPersonEntityId,
        string relationType )
    {
        var @enum = Guards.TryParse<RelatedPersonType>(relationType, nameof(relationType));
        return new RelatedPerson(relatedPersonEntityId, @enum);
    }
}
