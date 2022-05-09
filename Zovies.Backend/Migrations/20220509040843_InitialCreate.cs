using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zovies.Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieDetails",
                columns: table => new
                {
                    DetailsID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<float>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    MovieFilePath = table.Column<string>(type: "TEXT", nullable: false),
                    MovieCoverPath = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDetails", x => x.DetailsID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GenreName = table.Column<string>(type: "TEXT", nullable: false),
                    DetailsID = table.Column<int>(type: "INTEGER", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieName = table.Column<string>(type: "TEXT", nullable: false),
                    MovieDetailsDetailsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                    table.ForeignKey(
                        name: "FK_Movies_MovieDetails_MovieDetailsDetailsID",
                        column: x => x.MovieDetailsDetailsID,
                        principalTable: "MovieDetails",
                        principalColumn: "DetailsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    CastID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    MovieID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.CastID);
                    table.ForeignKey(
                        name: "FK_Actors_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieID",
                table: "Actors",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_DetailsID",
                table: "Genre",
                column: "DetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieDetailsDetailsID",
                table: "Movies",
                column: "MovieDetailsDetailsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "MovieDetails");
        }
    }
}
