using System.Text.Json.Serialization;
using WavensApi.Application.Common.Pagination;

namespace WavensApi.Application.Staffs
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(CreatedAtCursor), nameof(CreatedAtCursor))]
    public abstract record StaffKeysetCursor(
        StaffSortBy SortBy,
        Guid Id,
        SortOrder SortOrder
        )
    { }

    public sealed record CreatedAtCursor(DateTime CreatedAt, Guid Id, SortOrder SortOrder)
        : StaffKeysetCursor(StaffSortBy.CreatedAt, Id, SortOrder);

    public sealed record FirstNameCursor(string FirstName, Guid Id, SortOrder SortOrder)
        : StaffKeysetCursor(StaffSortBy.FirstName, Id, SortOrder);

    public sealed record LastNameCursor(string LastName, Guid Id, SortOrder SortOrder)
        : StaffKeysetCursor(StaffSortBy.LastName, Id, SortOrder);
}
