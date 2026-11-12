using FluentValidation;

namespace WavensApi.Presentation.Validations.Staffs
{
    public static class StaffValidationExtensions
    {
        public static IRuleBuilder<T, string?> ValidateFirstName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(2).WithMessage("{PropertyName} must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("{PropertyName} must be at most 50 characters long.");
        }

        public static IRuleBuilder<T, string?> ValidateLastName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(2).WithMessage("{PropertyName} must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("{PropertyName} must be at most 50 characters long.");
        }

        public static IRuleBuilder<T, DateOnly> ValidateBirthDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
        {
            return ruleBuilder
                .LessThan(DateOnly.FromDateTime(DateTime.Now.Date)).WithMessage("{PropertyName} must be a date in the past.");
        }
    }
}
