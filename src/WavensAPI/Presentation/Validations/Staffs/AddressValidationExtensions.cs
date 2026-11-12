using FluentValidation;
using System.Text.RegularExpressions;

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

        public static IRuleBuilder<T, string?> ValidateCep<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Length(8).WithMessage("{PropertyName} must be exactly 9 characters long.")
                .Must(cep => cep.All(char.IsAsciiDigit)).WithMessage("{PropertyName} must have only digits")
                .Must(cep => !string.IsNullOrWhiteSpace(cep) && cep.Any(c => c != cep[0])).WithMessage("{PropertyName} cannot have all digits the same");
        }
    }
}
