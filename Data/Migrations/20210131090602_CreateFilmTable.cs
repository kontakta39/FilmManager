using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmManager.Data.Migrations
{
    public partial class CreateFilmTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Director = table.Column<string>(maxLength: 50, nullable: false),
                    Music = table.Column<string>(maxLength: 50, nullable: false),
                    ReleaseYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");
        }
    }
}
