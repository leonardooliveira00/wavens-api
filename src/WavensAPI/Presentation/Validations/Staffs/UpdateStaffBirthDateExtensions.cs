using FluentValidation;

namespace WavensApi.Presentation.Validations.Staffs
{
    public static class UpdateStaffBirthDateExtensions
    {
        public static IRuleBuilder<T, DateOnly?> ValidateUpdateBirthDate<T>(this IRuleBuilder<T, DateOnly?> ruleBuilder)
        {
            return ruleBuilder
                .LessThan(DateOnly.FromDateTime(DateTime.Now.Date)).WithMessage("{PropertyName} must be a date in the past.");
        }
    }
}
