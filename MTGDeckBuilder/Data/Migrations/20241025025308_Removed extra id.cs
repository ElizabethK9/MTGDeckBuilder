using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGDeckBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class Removedextraid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_AspNetUsers_UserIDId",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GameDecks_AspNetUsers_UserIDId",
                table: "GameDecks");

            migrationBuilder.DropIndex(
                name: "IX_GameDecks_UserIDId",
                table: "GameDecks");

            migrationBuilder.DropIndex(
                name: "IX_GameCards_UserIDId",
                table: "GameCards");

            migrationBuilder.DropColumn(
                name: "UserIDId",
                table: "GameDecks");

            migrationBuilder.DropColumn(
                name: "UserIDId",
                table: "GameCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIDId",
                table: "GameDecks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIDId",
                table: "GameCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameDecks_UserIDId",
                table: "GameDecks",
                column: "UserIDId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_UserIDId",
                table: "GameCards",
                column: "UserIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_AspNetUsers_UserIDId",
                table: "GameCards",
                column: "UserIDId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameDecks_AspNetUsers_UserIDId",
                table: "GameDecks",
                column: "UserIDId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
