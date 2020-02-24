using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.DataAccess.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appearances_Characters_CharacterId",
                table: "Appearances");

            migrationBuilder.DropForeignKey(
                name: "FK_Appearances_Episodes_EpisodeId",
                table: "Appearances");

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Age", "Description", "Name", "PlanetId" },
                values: new object[,]
                {
                    { 2, 46, null, "Darth Vader", null },
                    { 4, 900, "Legendary Jedi Master", "Yoda", null }
                });

            migrationBuilder.InsertData(
                table: "Episodes",
                columns: new[] { "Id", "Date", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1999, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Episode I – The Phantom Menace" },
                    { 2, new DateTime(2002, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Episode II – Attack of the Clones" },
                    { 3, new DateTime(2005, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Episode III – Revenge of the Sith" }
                });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Capital planet of the Corellian system", "Corellia" },
                    { 2, "Desert planet", "Tatooine" },
                    { 3, "Extragalactic planet", "Kamino" },
                    { 4, "Planetoid", "Polis Massa" }
                });

            migrationBuilder.InsertData(
                table: "Appearances",
                columns: new[] { "EpisodeId", "CharacterId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Age", "Description", "Name", "PlanetId" },
                values: new object[,]
                {
                    { 1, 63, "Smuggler", "Han Solo", 1 },
                    { 5, 53, "One of the greatest Jedi Master", "Luke Skywalker", 2 },
                    { 3, 36, "Clone", "Boba Fett", 3 },
                    { 6, 54, null, "Princess Leia", 4 }
                });

            migrationBuilder.InsertData(
                table: "Appearances",
                columns: new[] { "EpisodeId", "CharacterId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 5 },
                    { 3, 3 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Friendships",
                columns: new[] { "CharacterId", "FriendId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 1 },
                    { 5, 4 },
                    { 3, 2 },
                    { 6, 1 },
                    { 6, 5 },
                    { 4, 6 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appearances_Characters_CharacterId",
                table: "Appearances",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appearances_Episodes_EpisodeId",
                table: "Appearances",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appearances_Characters_CharacterId",
                table: "Appearances");

            migrationBuilder.DropForeignKey(
                name: "FK_Appearances_Episodes_EpisodeId",
                table: "Appearances");

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Appearances",
                keyColumns: new[] { "EpisodeId", "CharacterId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumns: new[] { "CharacterId", "FriendId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Episodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Episodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Episodes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Appearances_Characters_CharacterId",
                table: "Appearances",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appearances_Episodes_EpisodeId",
                table: "Appearances",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
