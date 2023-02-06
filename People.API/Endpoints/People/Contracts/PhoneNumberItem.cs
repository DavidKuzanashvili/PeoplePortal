using FluentValidation;
using Microsoft.Extensions.Localization;
using People.Domain.Enums;

namespace People.API.Endpoints.People.Contracts;

public record PhoneNumberItem(
    string Type, 
    string CountryCode, 
    string PhoneCode)
{
    public class Validator : AbstractValidator<PhoneNumberItem>
    {
        public Validator(IStringLocalizer<CreatePersonRequest> localizer)
        {
            var phoneTypes = new string[]
            {
                PhoneNumberType.Mobile.ToString(),
                PhoneNumberType.Home.ToString(),
                PhoneNumberType.Office.ToString(),
            };

            RuleFor(x => x.Type)
                .Must(x => phoneTypes.Contains(x))
                .WithMessage("invalid.type");

            RuleFor(x => x.CountryCode)
                .NotNull()
                .WithMessage("required")
                .Matches(@"[0-9]")
                .WithMessage(localizer["invalid.number"]);

            RuleFor(x => x.PhoneCode)
                .NotNull()
                .WithMessage("required")
                .Matches(@"[0-9]")
                .WithMessage(localizer["invalid.number"]);
        }
    }
}
