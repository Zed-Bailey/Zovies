using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zovies.Backend.Migrations
{
    public partial class UpdateDetailsModelRelationshipToMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieDetails_MovieDetailsDetailsID",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieDetailsDetailsID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieDetailsDetailsID",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "MovieDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetails_MovieID",
                table: "MovieDetails",
                column: "MovieID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_MovieID",
                table: "MovieDetails",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_MovieID",
                table: "MovieDetails");

            migrationBuilder.DropIndex(
                name: "IX_MovieDetails_MovieID",
                table: "MovieDetails");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "MovieDetails");

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsDetailsID",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieDetailsDetailsID",
                table: "Movies",
                column: "MovieDetailsDetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieDetails_MovieDetailsDetailsID",
                table: "Movies",
                column: "MovieDetailsDetailsID",
                principalTable: "MovieDetails",
                principalColumn: "DetailsID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
