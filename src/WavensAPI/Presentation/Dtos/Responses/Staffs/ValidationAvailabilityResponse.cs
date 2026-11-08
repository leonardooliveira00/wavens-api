namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed record ValidationAvailabilityResponse
    {
        public bool? EmailAvailable { get; init; }
        public bool? ContactPhoneAvailable { get; init; }
    }
}
