using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGDeckBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixedmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserInventoryId",
                table: "GameCard",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard",
                column: "MID");

            migrationBuilder.CreateTable(
                name: "GameDeck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckPrice = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserInventoryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDeck_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDeck_AspNetUsers_UserInventoryId",
                        column: x => x.UserInventoryId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCard_UserInventoryId",
                table: "GameCard",
                column: "UserInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDeck_UserId",
                table: "GameDeck",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDeck_UserInventoryId",
                table: "GameDeck",
                column: "UserInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCard_AspNetUsers_UserInventoryId",
                table: "GameCard",
                column: "UserInventoryId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCard_AspNetUsers_UserInventoryId",
                table: "GameCard");

            migrationBuilder.DropTable(
                name: "GameDeck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard");

            migrationBuilder.DropIndex(
                name: "IX_GameCard_UserInventoryId",
                table: "GameCard");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserInventoryId",
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
    }
}
