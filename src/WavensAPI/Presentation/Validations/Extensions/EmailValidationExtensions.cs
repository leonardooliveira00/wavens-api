using FluentValidation;

namespace WavensApi.Presentation.Validations.Extensions
{
    public static class EmailValidationExtensions
    {
        public static IRuleBuilder<T, string?> ValidateEmail<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.")
                .MaximumLength(255).WithMessage("{PropertyName} must be at most 255 characters long.");
        }
    }
}
