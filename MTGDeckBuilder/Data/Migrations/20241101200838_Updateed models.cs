using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGDeckBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updateedmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCard_AspNetUsers_UserInventoryId",
                table: "GameCard");

            migrationBuilder.DropForeignKey(
                name: "FK_GameDeck_AspNetUsers_UserId",
                table: "GameDeck");

            migrationBuilder.DropForeignKey(
                name: "FK_GameDeck_AspNetUsers_UserInventoryId",
                table: "GameDeck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameDeck",
                table: "GameDeck");

            migrationBuilder.DropIndex(
                name: "IX_GameDeck_UserInventoryId",
                table: "GameDeck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard");

            migrationBuilder.DropIndex(
                name: "IX_GameCard_UserInventoryId",
                table: "GameCard");

            migrationBuilder.DropColumn(
                name: "DeckPrice",
                table: "GameDeck");

            migrationBuilder.DropColumn(
                name: "UserInventoryId",
                table: "GameDeck");

            migrationBuilder.DropColumn(
                name: "UserInventoryId",
                table: "GameCard");

            migrationBuilder.RenameTable(
                name: "GameDeck",
                newName: "GameDecks");

            migrationBuilder.RenameTable(
                name: "GameCard",
                newName: "GameCards");

            migrationBuilder.RenameIndex(
                name: "IX_GameDeck_UserId",
                table: "GameDecks",
                newName: "IX_GameDecks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCard_UserId",
                table: "GameCards",
                newName: "IX_GameCards_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameDecks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "GameDeckId",
                table: "GameCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameDecks",
                table: "GameDecks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCards",
                table: "GameCards",
                column: "MID");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_GameDeckId",
                table: "GameCards",
                column: "GameDeckId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_AspNetUsers_UserId",
                table: "GameCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_GameDecks_GameDeckId",
                table: "GameCards",
                column: "GameDeckId",
                principalTable: "GameDecks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameDecks_AspNetUsers_UserId",
                table: "GameDecks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_AspNetUsers_UserId",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_GameDecks_GameDeckId",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GameDecks_AspNetUsers_UserId",
                table: "GameDecks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameDecks",
                table: "GameDecks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCards",
                table: "GameCards");

            migrationBuilder.DropIndex(
                name: "IX_GameCards_GameDeckId",
                table: "GameCards");

            migrationBuilder.DropColumn(
                name: "GameDeckId",
                table: "GameCards");

            migrationBuilder.RenameTable(
                name: "GameDecks",
                newName: "GameDeck");

            migrationBuilder.RenameTable(
                name: "GameCards",
                newName: "GameCard");

            migrationBuilder.RenameIndex(
                name: "IX_GameDecks_UserId",
                table: "GameDeck",
                newName: "IX_GameDeck_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCards_UserId",
                table: "GameCard",
                newName: "IX_GameCard_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameDeck",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeckPrice",
                table: "GameDeck",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserInventoryId",
                table: "GameDeck",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserInventoryId",
                table: "GameCard",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameDeck",
                table: "GameDeck",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard",
                column: "MID");

            migrationBuilder.CreateIndex(
                name: "IX_GameDeck_UserInventoryId",
                table: "GameDeck",
                column: "UserInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCard_UserInventoryId",
                table: "GameCard",
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

            migrationBuilder.AddForeignKey(
                name: "FK_GameDeck_AspNetUsers_UserId",
                table: "GameDeck",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameDeck_AspNetUsers_UserInventoryId",
                table: "GameDeck",
                column: "UserInventoryId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
