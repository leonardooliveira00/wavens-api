using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WavensApi.Presentation.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value)) return default;

            if (DateOnly.TryParseExact(
                value, "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var isoDateFormat))
                return isoDateFormat;

            throw new JsonException($"Date does not have a valid format {isoDateFormat}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            Span<char> buffer = stackalloc Char[10];

            bool formattedSuccesfully = value.TryFormat(
                buffer,
                out int charsWritten,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture);

            if (!formattedSuccesfully) throw new JsonException("Failed to convert date");

            var formattedDate = buffer[..charsWritten];

            writer.WriteStringValue(formattedDate);
        }
    }
}
