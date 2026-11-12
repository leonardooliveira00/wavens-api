using FluentValidation;
using WavensApi.Presentation.Dtos.Requests.Staffs;

namespace WavensApi.Presentation.Validations.Staffs
{
    public sealed class RegisterAddressValidator : AbstractValidator<RegisterAddressRequest>
    {
        public RegisterAddressValidator()
        {
            RuleFor(address => address.Street)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateStreet();

            RuleFor(address => address.Number)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateNumber();

            RuleFor(address => address.Complement)
                .ValidateComplement();

            RuleFor(address => address.District)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateDistrict();

            RuleFor(address => address.State)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateState();

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateCity();

            RuleFor(address => address.Cep)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateCep();
        }
    }
}