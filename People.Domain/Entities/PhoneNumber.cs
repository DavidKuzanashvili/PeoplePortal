using People.Domain.Helpers;

namespace People.Domain.Entities;

public class PhoneNumber : BaseEntity<Guid>
{
    // For EF core
    private PhoneNumber() { }

    private PhoneNumber(
        string countryCode,
        string phoneCode,
        PhoneNumberType type) 
    {
        EntityId= Guid.NewGuid();
        CountryCode = countryCode;
        PhoneCode = phoneCode;
        Type = type;
    }

    public string CountryCode { get; private set; } = null!;
    public string PhoneCode { get; private set; } = null!;
    public PhoneNumberType Type { get; set; }

    public static PhoneNumber Create(
        string countryCode, 
        string phoneCode,
        string type)
    {
        return new PhoneNumber(
            countryCode, 
            phoneCode, 
            Guards.TryParse<PhoneNumberType>(type, nameof(type)));
    }

    public void Update(
        string countryCode,
        string phoneCode)
    {
        CountryCode = countryCode;
        PhoneCode = phoneCode;
    }
}
