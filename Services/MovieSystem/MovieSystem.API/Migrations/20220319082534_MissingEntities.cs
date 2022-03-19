using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystem.API.Migrations
{
    public partial class MissingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Movie",
                table: "Movies",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "ImdbUrl",
                schema: "Movie",
                table: "Movies",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500);

            migrationBuilder.AddColumn<int>(
                name: "DailyRentalRate",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberInStock",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                schema: "Movie",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DailyRentalRate", "GenreId", "NumberInStock", "Rating", "UpdatedDate" },
                values: new object[] { 5, 1, 20, 5, new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3372) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DailyRentalRate", "GenreId", "NumberInStock", "Rating", "UpdatedDate" },
                values: new object[] { 5, 1, 20, 5, new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3386) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DailyRentalRate", "GenreId", "NumberInStock", "Rating", "UpdatedDate" },
                values: new object[] { 5, 1, 20, 5, new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3390) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DailyRentalRate", "GenreId", "NumberInStock", "Rating", "UpdatedDate" },
                values: new object[] { 5, 1, 20, 5, new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3393) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DailyRentalRate", "GenreId", "NumberInStock", "Rating", "UpdatedDate" },
                values: new object[] { 5, 1, 20, 5, new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3396) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DailyRentalRate", "GenreId", "NumberInStock", "Rating", "UpdatedDate" },
                values: new object[] { 5, 1, 20, 5, new DateTime(2022, 3, 19, 10, 25, 33, 918, DateTimeKind.Local).AddTicks(3401) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyRentalRate",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenreId",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "NumberInStock",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "Movie",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "Movie",
                table: "Movies",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "ImdbUrl",
                schema: "Movie",
                table: "Movies",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Movie",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "UpdatedDate" },
                values: new object[] { "After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued across the galaxy by Darth Vader and bounty hunter Boba Fett.", new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3077) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "UpdatedDate" },
                values: new object[] { "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3115) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "UpdatedDate" },
                values: new object[] { "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3138) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "UpdatedDate" },
                values: new object[] { "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.", new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3151) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "UpdatedDate" },
                values: new object[] { "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3155) });

            migrationBuilder.UpdateData(
                schema: "Movie",
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "UpdatedDate" },
                values: new object[] { "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", new DateTime(2022, 3, 3, 13, 31, 16, 178, DateTimeKind.Local).AddTicks(3162) });
        }
    }
}
