using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.DataAccess.Migrations
{
    public partial class Episode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Planet_PlanetId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planet",
                table: "Planet");

            migrationBuilder.RenameTable(
                name: "Planet",
                newName: "Planets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planets",
                table: "Planets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appearances",
                columns: table => new
                {
                    EpisodeId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appearances", x => new { x.EpisodeId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_Appearances_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appearances_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appearances_CharacterId",
                table: "Appearances",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Planets_PlanetId",
                table: "Characters",
                column: "PlanetId",
                principalTable: "Planets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Planets_PlanetId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Appearances");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planets",
                table: "Planets");

            migrationBuilder.RenameTable(
                name: "Planets",
                newName: "Planet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planet",
                table: "Planet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Planet_PlanetId",
                table: "Characters",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
