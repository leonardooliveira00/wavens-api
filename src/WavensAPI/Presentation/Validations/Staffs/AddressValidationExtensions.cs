using FluentValidation;
using System.Text.RegularExpressions;
using WavensApi.Domain.Entities.Staffs.Addresses;

namespace WavensApi.Presentation.Validations.Staffs
{
    public static partial class AddressValidationExtensions
    {
        public static IRuleBuilder<T, string?> ValidateStreet<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(100).WithMessage("{PropertyName} must be at most 100 characters long.");
        }

        public static IRuleBuilder<T, string?> ValidateNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(10).WithMessage("{PropertyName} must be at most 10 characters long.");
        }

        public static IRuleBuilder<T, string?> ValidateComplement<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(100).WithMessage("{PropertyName} must be at most 100 characters long.");
        }

        public static IRuleBuilder<T, string?> ValidateDistrict<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(50).WithMessage("{PropertyName} must be at most 50 characters long.");
        }

        public static IRuleBuilder<T, string?> ValidateState<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(50).WithMessage("{PropertyName} must be at most 50 characters long.");
        }

        public static IRuleBuilder<T, string?> ValidateCity<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(50).WithMessage("{PropertyName} must be at most 50 characters long.");
        }

        public static IRuleBuilder<T, Cep?> ValidateCep<T>(this IRuleBuilder<T, Cep?> ruleBuilder)
        {
            return ruleBuilder
                .Must(cep => cep is null || cep.Value.Length != 8).WithMessage("{PropertyName} must be exactly 8 characters long.")
                .Must(cep => cep is null || cep.Value.All(char.IsAsciiDigit)).WithMessage("{PropertyName} must have only digits")
                .Must(cep => cep is null || !string.IsNullOrWhiteSpace(cep.Value) && cep.Value.Any(c => c != cep.Value[0])).WithMessage("{PropertyName} cannot have all digits the same")
                .When(cep => cep is not null);
        }
    }
}
