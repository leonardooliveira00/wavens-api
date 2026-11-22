using WavensApi.Domain.Entities.Staffs;

namespace WavensApi.Presentation.Dtos.Requests.Staffs
{
    public sealed record ValidateAvailability
    {
        public string? Email { get; init; }
        public MobilePhoneNumber? ContactPhone { get; init; }
    }
}
