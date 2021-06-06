using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MovieCrewRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrewMovieCrew",
                columns: table => new
                {
                    CrewsId = table.Column<int>(type: "int", nullable: false),
                    MovieCrewDepartment = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    MovieCrewJob = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewMovieCrew", x => new { x.CrewsId, x.MovieCrewDepartment, x.MovieCrewJob });
                    table.ForeignKey(
                        name: "FK_CrewMovieCrew_Crew_CrewsId",
                        column: x => x.CrewsId,
                        principalTable: "Crew",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewMovieCrew_MovieCrew_MovieCrewDepartment_MovieCrewJob",
                        columns: x => new { x.MovieCrewDepartment, x.MovieCrewJob },
                        principalTable: "MovieCrew",
                        principalColumns: new[] { "Department", "Job" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCrew_MovieId",
                table: "MovieCrew",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewMovieCrew_MovieCrewDepartment_MovieCrewJob",
                table: "CrewMovieCrew",
                columns: new[] { "MovieCrewDepartment", "MovieCrewJob" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCrew_Movie_MovieId",
                table: "MovieCrew",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCrew_Movie_MovieId",
                table: "MovieCrew");

            migrationBuilder.DropTable(
                name: "CrewMovieCrew");

            migrationBuilder.DropIndex(
                name: "IX_MovieCrew_MovieId",
                table: "MovieCrew");
        }
    }
}
