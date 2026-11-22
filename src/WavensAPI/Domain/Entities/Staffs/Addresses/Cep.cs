namespace WavensApi.Domain.Entities.Staffs.Addresses
{
    public sealed record Cep
    {
        public string Value { get; }

        private Cep(string value)
        {
            Value = value;
        }

        public static Cep Create(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep) ||
                cep.Length != 8 ||
                !cep.All(char.IsAsciiDigit) ||
                cep.Distinct().Count() == 1
                )
                throw new ArgumentException("Invalid CEP format");

            return new Cep(cep);
        }
    }
}
