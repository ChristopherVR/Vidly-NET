using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystem.API.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movie");

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImdbUrl = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetails_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetails_PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetails_HomeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetails_ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFavouriteMovies",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavouriteMovies_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Movie",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Movie",
                table: "Movies",
                columns: new[] { "Id", "Description", "ImdbUrl", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[,]
                {
                    { 1, "After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued across the galaxy by Darth Vader and bounty hunter Boba Fett.", "https://www.imdb.com/title/tt0080684/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_15", "Star Wars: Episove V - The Empire Strikes Back (1980)", new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4350), "Initial" },
                    { 2, "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "https://www.imdb.com/title/tt0167260/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_7", "The Lord of the Rings: The Return of the King (2003)", new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4368), "Initial" },
                    { 3, "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", "https://www.imdb.com/title/tt0468569/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_4", "The Godfather: Part II (1974)", new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4372), "Initial" },
                    { 4, "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.", "https://www.imdb.com/title/tt0071562/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_3", "The Godfather (1972)", new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4378), "Initial" },
                    { 5, "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "https://www.imdb.com/title/tt0068646/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_2", "Schindler's List (1993)", new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4392), "Initial" },
                    { 6, "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", "https://www.imdb.com/title/tt0108052/?pf_rd_m=A2FGELUUNOQJNL&pf_rd_p=9703a62d-b88a-4e30-ae12-90fcafafa3fc&pf_rd_r=NQ69CZ5V8W1CDCAXTJAY&pf_rd_s=center-1&pf_rd_t=15506&pf_rd_i=top&ref_=chttp_tt_6", "Fight Club (1999)", new DateTime(2022, 2, 5, 18, 13, 49, 322, DateTimeKind.Local).AddTicks(4398), "Initial" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteMovies_UserId",
                schema: "Movie",
                table: "UserFavouriteMovies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "Movie",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "UserFavouriteMovies",
                schema: "Movie");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Movie");
        }
    }
}
