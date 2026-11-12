using FluentValidation;
using WavensApi.Presentation.Dtos.Requests.Staffs;
using WavensApi.Presentation.Validations.Extensions;

namespace WavensApi.Presentation.Validations.Staffs
{
    public sealed class RegisterStaffValidator : AbstractValidator<RegisterStaffRequest>
    {
        public RegisterStaffValidator()
        {
            RuleFor(staff => staff.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateFirstName();

            RuleFor(staff => staff.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateLastName();

            RuleFor(staff => staff.BirthDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateBirthDate();

            RuleFor(staff => staff.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateEmail();

            RuleFor(staff => staff.ContactPhone)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .ValidateContactPhone();

            RuleFor(staff => staff.Address)
                .NotNull().WithMessage("{PropertyName} is required.")
                .SetValidator(new RegisterAddressValidator());
        }
    }
}