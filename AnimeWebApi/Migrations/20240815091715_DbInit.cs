using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWebApi.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_animes_CharacterId",
                table: "animes");

            migrationBuilder.CreateIndex(
                name: "IX_animes_CharacterId",
                table: "animes",
                column: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_animes_CharacterId",
                table: "animes");

            migrationBuilder.CreateIndex(
                name: "IX_animes_CharacterId",
                table: "animes",
                column: "CharacterId",
                unique: true);
        }
    }
}
