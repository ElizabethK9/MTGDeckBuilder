using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTGDeckBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_AspNetUsers_UserId",
                table: "GameCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCards",
                table: "GameCards");

            migrationBuilder.DropColumn(
                name: "CardMID",
                table: "GameCards");

            migrationBuilder.RenameTable(
                name: "GameCards",
                newName: "GameCard");

            migrationBuilder.RenameColumn(
                name: "cardImageURL",
                table: "GameCard",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "CardType",
                table: "GameCard",
                newName: "Subtype");

            migrationBuilder.RenameColumn(
                name: "CardSubtype",
                table: "GameCard",
                newName: "Set");

            migrationBuilder.RenameColumn(
                name: "CardSet",
                table: "GameCard",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "CardPrice",
                table: "GameCard",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "CardName",
                table: "GameCard",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_GameCards_UserId",
                table: "GameCard",
                newName: "IX_GameCard_UserId");

            migrationBuilder.AlterColumn<float>(
                name: "ManaCost",
                table: "GameCard",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatureToughness",
                table: "GameCard",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreaturePower",
                table: "GameCard",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CollectorNumber",
                table: "GameCard",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MID",
                table: "GameCard",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard",
                column: "MID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameCard_AspNetUsers_UserId",
                table: "GameCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameCard",
                table: "GameCard");

            migrationBuilder.DropColumn(
                name: "MID",
                table: "GameCard");

            migrationBuilder.RenameTable(
                name: "GameCard",
                newName: "GameCards");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "GameCards",
                newName: "cardImageURL");

            migrationBuilder.RenameColumn(
                name: "Subtype",
                table: "GameCards",
                newName: "CardType");

            migrationBuilder.RenameColumn(
                name: "Set",
                table: "GameCards",
                newName: "CardSubtype");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "GameCards",
                newName: "CardPrice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GameCards",
                newName: "CardName");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "GameCards",
                newName: "CardSet");

            migrationBuilder.RenameIndex(
                name: "IX_GameCard_UserId",
                table: "GameCards",
                newName: "IX_GameCards_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "ManaCost",
                table: "GameCards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatureToughness",
                table: "GameCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreaturePower",
                table: "GameCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CollectorNumber",
                table: "GameCards",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardMID",
                table: "GameCards",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameCards",
                table: "GameCards",
                column: "CardMID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_AspNetUsers_UserId",
                table: "GameCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
