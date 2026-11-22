using Microsoft.EntityFrameworkCore;

namespace WavensApi.Domain.Entities.Staffs.Addresses
{
    [Owned]
    public class Address
    {
        public Guid StaffId { get; set; }
        public required string Street { get; set; }
        public required string Number { get; set; }
        public string? Complement { get; set; }
        public required string District { get; set; }
        public required string State { get; set; }
        public required string City { get; set; }
        public required Cep Cep { get; set; }
    }
}
