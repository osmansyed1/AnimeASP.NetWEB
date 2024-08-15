using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnimeWebApi.Migrations
{
    /// <inheritdoc />
    public partial class initialD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 5, "Notorious Vllain", "Shukuna" },
                    { 6, "Scary Villain & Hero", "Ivar The Boneless" }
                });

            migrationBuilder.InsertData(
                table: "directors",
                columns: new[] { "Id", "Name", "achievement" },
                values: new object[] { 5, "Roy", "Indian Award" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "directors",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
