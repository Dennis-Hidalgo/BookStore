using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Corrected_Marker_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Markers");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Markers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Markers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MarkerType",
                table: "Markers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "MarkerType",
                table: "Markers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Markers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
