using FluentValidation;
using WavensApi.Presentation.Dtos.Requests.Staffs;

namespace WavensApi.Presentation.Validations.Staffs
{
    public sealed class UpdateAddressValidator : AbstractValidator<UpdateAddressRequest>
    {
        public UpdateAddressValidator()
        {
            RuleFor(address => address.Street).ValidateStreet();

            RuleFor(address => address.Number).ValidateNumber();

            RuleFor(address => address.Complement).ValidateComplement();

            RuleFor(address => address.District).ValidateDistrict();

            RuleFor(address => address.State).ValidateState();

            RuleFor(address => address.City).ValidateCity();

            RuleFor(address => address.Cep).ValidateCep();
        }
    }
}
