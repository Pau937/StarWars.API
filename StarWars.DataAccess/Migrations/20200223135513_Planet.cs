using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.DataAccess.Migrations
{
    public partial class Planet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanetId",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlanetId",
                table: "Characters",
                column: "PlanetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Planet_PlanetId",
                table: "Characters",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Planet_PlanetId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Planet");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PlanetId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PlanetId",
                table: "Characters");
        }
    }
}
