using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zovies.Backend.Migrations
{
    public partial class UpdateGenresModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.AddColumn<string>(
                name: "MovieGenres",
                table: "MovieDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieGenres",
                table: "MovieDetails");

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    CastID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.CastID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DetailsID = table.Column<int>(type: "INTEGER", nullable: true),
                    GenreName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                    table.ForeignKey(
                        name: "FK_Genre_MovieDetails_DetailsID",
                        column: x => x.DetailsID,
                        principalTable: "MovieDetails",
                        principalColumn: "DetailsID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_DetailsID",
                table: "Genre",
                column: "DetailsID");
        }
    }
}
