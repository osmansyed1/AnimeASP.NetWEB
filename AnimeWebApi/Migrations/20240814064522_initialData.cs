using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnimeWebApi.Migrations
{
    /// <inheritdoc />
    public partial class initialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    achievement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "viewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Critic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "animes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    directorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_animes_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_animes_directors_directorID",
                        column: x => x.directorID,
                        principalTable: "directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "animeviews",
                columns: table => new
                {
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    ViewerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animeviews", x => new { x.AnimeId, x.ViewerId });
                    table.ForeignKey(
                        name: "FK_animeviews_animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_animeviews_viewers_ViewerId",
                        column: x => x.ViewerId,
                        principalTable: "viewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "characters",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Hero", "Naruto" },
                    { 2, "Protagonist", "Gojo" },
                    { 3, "Climax Hero", "NaNami" },
                    { 4, "Greatest Anagonist Ever", "Madara Uchicha" }
                });

            migrationBuilder.InsertData(
                table: "directors",
                columns: new[] { "Id", "Name", "achievement" },
                values: new object[,]
                {
                    { 1, "Jhon", "Golden Globe Awards" },
                    { 2, "Roy", "CFF" },
                    { 3, "Shawn", "CFF" },
                    { 4, "Ar Rhaman", "Freanch Award" }
                });

            migrationBuilder.InsertData(
                table: "viewers",
                columns: new[] { "Id", "Critic", "Name" },
                values: new object[,]
                {
                    { 1, "rank 1", "Elite" },
                    { 2, "rank 10", "Jackson" },
                    { 3, "rank 111", "Hinkle" }
                });

            migrationBuilder.InsertData(
                table: "animes",
                columns: new[] { "Id", "CharacterId", "Details", "Genre", "Title", "directorID" },
                values: new object[,]
                {
                    { 1, 1, "a panese manga series written and illustrated by Gege Akutami", "Dark Adventure", "Jujutsu Kaisen", 1 },
                    { 2, 2, "illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki", "Action, Adventure,Fantasy", "Naruto", 2 },
                    { 3, 3, "In the Warring States Period of ancient China (475–221 BCE), Shin and Hyou are war-orphans in the kingdom of Qin", "Action,Diplomacy,Military", "Kingdom", 3 },
                    { 4, 4, "Hellsing, a British sponsored secret organisation, is in charge of keeping a check on all vampire", "Horror Adventure", "Hellsing Ultimate", 4 }
                });

            migrationBuilder.InsertData(
                table: "animeviews",
                columns: new[] { "AnimeId", "ViewerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_animes_CharacterId",
                table: "animes",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_animes_directorID",
                table: "animes",
                column: "directorID");

            migrationBuilder.CreateIndex(
                name: "IX_animeviews_ViewerId",
                table: "animeviews",
                column: "ViewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animeviews");

            migrationBuilder.DropTable(
                name: "animes");

            migrationBuilder.DropTable(
                name: "viewers");

            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "directors");
        }
    }
}
