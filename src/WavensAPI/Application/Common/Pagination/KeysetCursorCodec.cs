using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;
using WavensApi.Application.Staffs;

namespace WavensApi.Application.Common.Pagination
{
    public static class KeysetCursorCodec
    {
        public static string Encode<T>(T cursor, Type baseType)
        {
            var encoded = JsonSerializer.Serialize(cursor, baseType);

            var bytes = Encoding.UTF8.GetBytes(encoded);

            return WebEncoders.Base64UrlEncode(bytes);
        }

        public static bool TryDecode<T>(string encodedCursor, Type baseType, out T? cursor)
        {
            cursor = default;

            try
            {
                var bytes = WebEncoders.Base64UrlDecode(encodedCursor);

                var json = Encoding.UTF8.GetString(bytes);

                var decoded = JsonSerializer.Deserialize(json, baseType);

                if (decoded is T result)
                {
                    cursor = result;
                    return true;
                }

                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
