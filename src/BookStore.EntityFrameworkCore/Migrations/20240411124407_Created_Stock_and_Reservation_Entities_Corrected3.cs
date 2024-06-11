using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Created_Stock_and_Reservation_Entities_Corrected3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AppBooks_LibroID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_AppBooks_LibroId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LibroID",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "LibroId",
                table: "Stocks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_LibroId",
                table: "Stocks",
                newName: "IX_Stocks_BookId");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Reservations",
                newName: "BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AppBooks_BookId",
                table: "Reservations",
                column: "BookId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_AppBooks_BookId",
                table: "Stocks",
                column: "BookId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AppBooks_BookId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_AppBooks_BookId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Stocks",
                newName: "LibroId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_BookId",
                table: "Stocks",
                newName: "IX_Stocks_LibroId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Reservations",
                newName: "UsuarioID");

            migrationBuilder.AddColumn<Guid>(
                name: "LibroID",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "LibroID", "UsuarioID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AppBooks_LibroID",
                table: "Reservations",
                column: "LibroID",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_AppBooks_LibroId",
                table: "Stocks",
                column: "LibroId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
