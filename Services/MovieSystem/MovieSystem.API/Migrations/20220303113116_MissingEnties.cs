using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystem.API.Migrations
{
    public partial class MissingEnties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Liked",
                schema: "Movie",
                table: "UserFavouriteMovies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3077));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3115));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3138));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3151));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3155));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedDate",
                value: new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3162));

            migrationBuilder.InsertData(
                schema: "Movie",
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "Name", "Surname", "UpdatedDate", "UpdatedUser", "Username", "UserDetails_Address", "UserDetails_HomeNumber", "UserDetails_ImageUrl", "UserDetails_PersonalNumber" },
                values: new object[] { 1, "", "Christopher", "van Rooyen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "initial", "ChristopherVR", "12th Avenue nr 17", "0000 632198", null, "+27 79 507 2155" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres",
                schema: "Movie");

            migrationBuilder.DeleteData(
                schema: "Movie",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Liked",
                schema: "Movie",
                table: "UserFavouriteMovies");

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4350));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedDate",
                value: new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4368));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedDate",
                value: new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4372));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedDate",
                value: new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedDate",
                value: new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4392));

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedDate",
                value: new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4398));
        }
    }
}
