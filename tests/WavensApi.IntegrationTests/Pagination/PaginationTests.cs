using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using WavensApi.Application.Common.Pagination;
using WavensApi.IntegrationTests.Infrastructure;
using WavensApi.Application.Staffs;

namespace WavensAPI.IntegrationTests.Pagination
{
    public class PaginationTests(WavensWebApplicationFactory factory) : IClassFixture<WavensWebApplicationFactory>
    {
        [Fact]
        public void Encode_ShouldReturnEncodedCursor()
        {
            var cursor = new CreatedAtCursor(
                Id: Guid.NewGuid(),
                CreatedAt: DateTime.UtcNow,
                SortOrder: SortOrder.Desc
            );

            var encoded = KeysetCursorCodec.Encode(cursor, typeof(StaffKeysetCursor));

            Assert.Contains("CreatedAtCursor",
                Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encoded)));

            var decodedSuccessfully =
            KeysetCursorCodec.TryDecode<StaffKeysetCursor>(
                encoded, 
                typeof(StaffKeysetCursor), 
                out var decoded);

            Assert.True(decodedSuccessfully);
            Assert.IsType<CreatedAtCursor>(decoded);
        }
    }
}
