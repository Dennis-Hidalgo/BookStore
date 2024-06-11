using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Created_Stock_and_Reservation_Entities_Corrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LibroID",
                table: "Reservations",
                column: "LibroID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AppBooks_LibroID",
                table: "Reservations",
                column: "LibroID",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AppBooks_LibroID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_LibroID",
                table: "Reservations");
        }
    }
}
