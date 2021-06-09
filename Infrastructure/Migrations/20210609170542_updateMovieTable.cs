using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class updateMovieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Crew",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Cast",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MovieId",
                table: "Genre",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_MovieId",
                table: "Crew",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Cast_MovieId",
                table: "Cast",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cast_Movie_MovieId",
                table: "Cast",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Crew_Movie_MovieId",
                table: "Crew",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Movie_MovieId",
                table: "Genre",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cast_Movie_MovieId",
                table: "Cast");

            migrationBuilder.DropForeignKey(
                name: "FK_Crew_Movie_MovieId",
                table: "Crew");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Movie_MovieId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_MovieId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Crew_MovieId",
                table: "Crew");

            migrationBuilder.DropIndex(
                name: "IX_Cast_MovieId",
                table: "Cast");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Crew");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Cast");
        }
    }
}
