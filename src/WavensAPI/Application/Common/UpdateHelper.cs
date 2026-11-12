namespace WavensApi.Application.Common
{
    public static class UpdateHelper
    {
        public static void UpdateIfProvided<T>(T? value, Action<T> update)
            where T : class
        {
            if (value is not null) update(value);
        }

        public static void UpdateIfProvided<T>(T? value, Action<T> update)
            where T : struct
        {
            if (value.HasValue) update(value.Value);
        }
    }
}
