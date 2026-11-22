using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using WavensApi.Domain.Entities.Staffs.Addresses;

namespace WavensApi.Presentation.Converters
{
    public class CepJsonConverter : JsonConverter<Cep>
    {
        public override Cep? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value)) throw new JsonException("Invalid cep format");

            return Cep.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, Cep value, JsonSerializerOptions options)
        {
            if(!string.IsNullOrWhiteSpace(value.Value) && value.Value.Length == 8)
            {
                ReadOnlySpan<char> span = value.Value.AsSpan();
                writer.WriteStringValue($"{span[..5]}-{span[5..8]}");
            } else
            {
                writer.WriteStringValue(value.Value);
            }
        }
    }
}