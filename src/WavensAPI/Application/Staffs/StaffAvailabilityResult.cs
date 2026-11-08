namespace WavensApi.Application.Staffs
{
    public sealed record StaffAvailabilityResult
    {
        public bool? EmailAvailable { get; init; }
        public bool? ContactPhoneAvailable { get; init; }
    }
}
