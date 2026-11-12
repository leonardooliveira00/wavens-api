using FluentValidation;
using System.Text.RegularExpressions;

namespace WavensApi.Presentation.Validations.Extensions
{
    public static partial class PhoneValidationExtensions
    {
        [GeneratedRegex(@"^(?:1[1-9]|2[12478]|3[1-578]|4[1-9]|5[1345]|6[1-9]|7[134579]|8[1-9]|9[1-9])9[0-9]{8}$")]
        private static partial Regex BrazilianPhoneRegex();
        public static IRuleBuilderOptions<T, string?> ValidateContactPhone<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Length(11).WithMessage("{PropertyName} must have exactly 11 digits.")
                .Matches(BrazilianPhoneRegex()).WithMessage("{PropertyName} must have a valid DDD and 9 digits.")
                .Must(phone => phone[..3].Distinct().Count() > 1).WithMessage("{PropertyName} can't have all digits the same.");
        }
    }
}