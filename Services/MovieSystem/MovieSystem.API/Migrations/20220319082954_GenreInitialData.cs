using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystem.API.Migrations
{
    public partial class GenreInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Movie",
                table: "Genres",
                columns: new[] { "Id", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[,]
                {
                    { 1, "Rock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" },
                    { 2, "Pop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" },
                    { 3, "Sci-Fi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" },
                    { 4, "Action", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" },
                    { 5, "Thriller", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" },
                    { 6, "Comedy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" },
                    { 7, "Horror", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial" }
                });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3372));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3386));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3390));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3393));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3396));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3401));
        }
    }
}
