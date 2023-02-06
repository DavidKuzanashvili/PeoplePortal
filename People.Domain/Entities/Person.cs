using People.Domain.Helpers;

namespace People.Domain.Entities;

public class Person : BaseAuditableEntity<Guid>
{
    private List<PhoneNumber> _phoneNumbers = new();
    private List<RelatedPerson> _relatedPeople = new();

    // For EF core
    private Person() { }

    private Person(
        string name,
        string surname,
        GenderType gender,
        City city,
        string personalNumber,
        DateTime dateOfBirth,
        string filePath,
        List<PhoneNumber> phoneNumbers)
    {
        EntityId = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Gender = gender;
        City = city;
        PersonalNumber = personalNumber;
        DateOfBirth = dateOfBirth;
        ProfileImagePath = filePath;
        _phoneNumbers = phoneNumbers;
    }

    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
    public GenderType Gender { get; private set; }
    public City City { get; private set; } = null!;
    public string PersonalNumber { get; private set; } = null!;
    public DateTime DateOfBirth { get; private set; }
    public string ProfileImagePath { get; set; } = null!;

    public IReadOnlyCollection<PhoneNumber> PhoneNumbers
        => _phoneNumbers.AsReadOnly();

    public IReadOnlyCollection<RelatedPerson> RelatedPeople
        => _relatedPeople.AsReadOnly();

    public static Person Create(
        string name,
        string surname,
        string gender,
        City city,
        string personalNumber,
        DateTime dateOfBirth,
        string filePath,
        List<PhoneNumber> phoneNumbers)
    {
        return new Person(
            name, 
            surname, 
            Guards.TryParse<GenderType>(gender, nameof(gender)),
            city, 
            personalNumber, 
            dateOfBirth,
            filePath,
            phoneNumbers);
    }

    public void Update(
        string name,
        string surname,
        string gender,
        City city,
        string personalNumber,
        DateTime dateOfBirth,
        List<PhoneNumber> phoneNumbers)
    {
        Name = name;
        Surname = surname;
        Gender = Guards.TryParse<GenderType>(gender, nameof(gender));
        City = city;
        PersonalNumber = personalNumber;
        DateOfBirth = dateOfBirth;

        _phoneNumbers.ForEach(pn =>
        {
            var data = phoneNumbers.First(x => x.Type == pn.Type);
            pn.Update(data.CountryCode, data.PhoneCode);
        });
    }

    public void SetImagePath(string imagePath)
    {
        ProfileImagePath = imagePath;
    }

    public void ChangeRelatedPeople(List<RelatedPerson> relatedPeople) 
    {
        _relatedPeople.RemoveAll(x => 
            !relatedPeople.Any(y => y.RelatedPersonEntityId == x.RelatedPersonEntityId));

        var peopleToAdd = relatedPeople
            .Where(x => !_relatedPeople.Any(y => x.RelatedPersonEntityId == y.RelatedPersonEntityId))
            .ToList();
        _relatedPeople.AddRange(relatedPeople);
    }
}
