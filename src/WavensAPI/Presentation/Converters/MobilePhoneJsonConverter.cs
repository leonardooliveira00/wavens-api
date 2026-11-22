using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using WavensApi.Domain.Entities.Staffs;

namespace WavensApi.Presentation.Converters
{
    public class MobilePhoneJsonConverter : JsonConverter<MobilePhoneNumber>
    {
        public override MobilePhoneNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value)) throw new JsonException("Invalid mobile phone number value.");

            return MobilePhoneNumber.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, MobilePhoneNumber value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrWhiteSpace(value.Value) && value.Value.Length == 11)
            {
                ReadOnlySpan<char> span = value.Value.AsSpan();
                writer.WriteStringValue($"({span[..2]}) {span[2..7]}-{span[7..11]}");
            }
            else
            {
                writer.WriteStringValue(value.Value);
            }
        }
    }
}
