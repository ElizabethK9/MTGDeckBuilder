using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGDeckBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedgamecardsandgamedeckstothedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameCards",
                columns: table => new
                {
                    CardMID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardSubtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManaCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardSet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardPrice = table.Column<int>(type: "int", nullable: true),
                    CreaturePower = table.Column<int>(type: "int", nullable: true),
                    CreatureToughness = table.Column<int>(type: "int", nullable: true),
                    CollectorNumber = table.Column<int>(type: "int", nullable: true),
                    cardImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserIDId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCards", x => x.CardMID);
                    table.ForeignKey(
                        name: "FK_GameCards_AspNetUsers_UserIDId",
                        column: x => x.UserIDId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameCards_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameDecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckPrice = table.Column<int>(type: "int", nullable: false),
                    UserIDId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDecks_AspNetUsers_UserIDId",
                        column: x => x.UserIDId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameDecks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_UserId",
                table: "GameCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_UserIDId",
                table: "GameCards",
                column: "UserIDId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDecks_UserId",
                table: "GameDecks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDecks_UserIDId",
                table: "GameDecks",
                column: "UserIDId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCards");

            migrationBuilder.DropTable(
                name: "GameDecks");
        }
    }
}
