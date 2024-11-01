using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGDeckBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedcardsanddecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard");

            migrationBuilder.DropTable(
                name: "GameDecks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard");

            migrationBuilder.RenameTable(
                name: "GameCard",
                newName: "UserInventory");

            migrationBuilder.RenameIndex(
                name: "IX_GameCard_UserId",
                table: "UserInventory",
                newName: "IX_UserInventory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInventory",
                table: "UserInventory",
                column: "MID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInventory_AspNetUsers_UserId",
                table: "UserInventory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInventory_AspNetUsers_UserId",
                table: "UserInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInventory",
                table: "UserInventory");

            migrationBuilder.RenameTable(
                name: "UserInventory",
                newName: "GameCard");

            migrationBuilder.RenameIndex(
                name: "IX_UserInventory_UserId",
                table: "GameCard",
                newName: "IX_GameCard_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard",
                column: "MID");

            migrationBuilder.CreateTable(
                name: "GameDecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeckFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDecks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameDecks_UserId",
                table: "GameDecks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
