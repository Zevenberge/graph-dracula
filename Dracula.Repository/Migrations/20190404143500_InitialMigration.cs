using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dracula.Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Iso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    NationalityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actor_Country_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ReleaseYear = table.Column<int>(nullable: false),
                    CountryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Film_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Casting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilmId = table.Column<Guid>(nullable: true),
                    ActorId = table.Column<Guid>(nullable: true),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Casting_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Casting_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_NationalityId",
                table: "Actor",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Casting_ActorId",
                table: "Casting",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Casting_FilmId",
                table: "Casting",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Film_CountryId",
                table: "Film",
                column: "CountryId");

            migrationBuilder.Sql(@"
            insert into Country(Id, Name, Iso)
            values (newid(), 'Netherlands', 'NLD'),
              (newid(), 'United States', 'USA'),
              (newid(), 'Japan', 'JPN'),
              (newid(), 'Belgium', 'BEL'),
              (newid(), 'Germany', 'DEU'),
              (newid(), 'France', 'FRA'),
              (newid(), 'China', 'CHN'),
              (newid(), 'Australia', 'AUS')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Casting");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
