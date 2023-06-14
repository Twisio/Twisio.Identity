using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWISIO.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddoAuthids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VKontakteId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YandexId",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VKontakteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "YandexId",
                table: "Users");
        }
    }
}
