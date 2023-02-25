using FluentValidation;
using Microsoft.Extensions.Localization;
using People.API.ValidationExtensions;
using People.Domain.Enums;

namespace People.API.Endpoints.People.Contracts;

public record UpdatePersonRequest(
    Guid Id,
    string Name,
    string Surname,
    string Gender,
    string CityName,
    string PersonalNumber,
    DateTime DateOfBirth,
    List<PhoneNumberItem> PhoneNumbers)
{
    public class Validator : AbstractValidator<UpdatePersonRequest>
    {
        public Validator(IStringLocalizer<UpdatePersonRequest> localizer)
        {
            var genderNames = new string[]
            {
                GenderType.Male.ToString(),
                GenderType.Female.ToString(),
            };

            var minAllowedDOB = DateTime.UtcNow.Date.AddYears(-18);

            RuleFor(x => x.Name)
                .MinimumLength(2)
                .WithMessage(localizer["invalid.min.length2"])
                .MaximumLength(50)
                .WithMessage(localizer["invalid.max.length50"])
                .MatchesValidAlphabet();

            RuleFor(x => x.Surname)
                .MinimumLength(2)
                .WithMessage(localizer["invalid.min.length2"])
                .MaximumLength(50)
                .WithMessage(localizer["invalid.max.length50"])
                .MatchesValidAlphabet();

            RuleFor(x => x.Gender)
                .Must(x => genderNames.Contains(x))
                .WithMessage(localizer["invalid.gender"]);

            RuleFor(x => x.CityName)
                .NotNull()
                .WithMessage(localizer["required.cityName"]);

            RuleFor(x => x.PersonalNumber)
                .Length(11)
                .WithMessage("invalid.length11")
                .Matches(@"[0-9]")
                .WithMessage(localizer["invalid.onlynumbers"]);

            RuleFor(x => x.DateOfBirth)
                .Must(x => x.Date <= minAllowedDOB)
                .WithErrorCode(localizer["invalid.age"]);

            RuleFor(x => x.PhoneNumbers)
                .Must(x => x.Count == 3)
                .WithMessage(localizer["invalid.phoneNumber.length3"]);
        }
    }
}
