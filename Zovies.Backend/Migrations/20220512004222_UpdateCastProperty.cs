using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zovies.Backend.Migrations
{
    public partial class UpdateCastProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieID",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieID",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "MovieCast",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieCast",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "Actors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieID",
                table: "Actors",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieID",
                table: "Actors",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "MovieID");
        }
    }
}
