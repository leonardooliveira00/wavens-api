using System.Text.RegularExpressions;

namespace WavensApi.Domain.Entities.Staffs
{
    public sealed partial record MobilePhoneNumber
    {
        public string Value { get; }

        private MobilePhoneNumber(string value)
        {
            Value = value;
        }

        [GeneratedRegex(@"^(?:1[1-9]|2[12478]|3[1-578]|4[1-9]|5[1345]|6[1-9]|7[134579]|8[1-9]|9[1-9])9[0-9]{8}$")]
        private static partial Regex BrazilianPhoneRegex();

        public static MobilePhoneNumber Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) ||
                !BrazilianPhoneRegex().IsMatch(phone) ||
                phone.Distinct().Count() == 1)
                throw new ArgumentException("Invalid mobile phone number.");

            return new MobilePhoneNumber(phone);
        }
    }
}