using FluentValidation;
using WavensApi.Presentation.Dtos.Requests.Staffs;
using WavensApi.Presentation.Validations.Extensions;

namespace WavensApi.Presentation.Validations.Staffs
{
    public sealed class UpdateStaffValidator : AbstractValidator<UpdateStaffRequest>
    {
        public UpdateStaffValidator()
        {
            RuleFor(staff => staff.FirstName).ValidateFirstName();

            RuleFor(staff => staff.LastName).ValidateLastName();

            RuleFor(staff => staff.BirthDate).ValidateUpdateBirthDate();

            RuleFor(staff => staff.Email).ValidateEmail();

            RuleFor(staff => staff.ContactPhone).ValidateContactPhone();

            RuleFor(staff => staff.Address!).SetValidator(new UpdateAddressValidator());
        }
    }
}
