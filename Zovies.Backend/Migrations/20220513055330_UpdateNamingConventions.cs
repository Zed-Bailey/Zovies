using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zovies.Backend.Migrations
{
    public partial class UpdateNamingConventions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "MovieID",
                table: "Movies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "DetailsID",
                table: "MovieDetails",
                newName: "DetailsId");

            migrationBuilder.AlterColumn<int>(
                name: "DetailsId",
                table: "MovieDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_DetailsId",
                table: "MovieDetails",
                column: "DetailsId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_DetailsId",
                table: "MovieDetails");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "MovieID");

            migrationBuilder.RenameColumn(
                name: "DetailsId",
                table: "MovieDetails",
                newName: "DetailsID");

            migrationBuilder.AlterColumn<int>(
                name: "DetailsID",
                table: "MovieDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

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
    }
}
