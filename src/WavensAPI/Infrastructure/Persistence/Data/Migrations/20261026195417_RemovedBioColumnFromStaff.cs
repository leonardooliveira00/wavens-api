using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WavensApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBioColumnFromStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Staffs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Staffs",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
