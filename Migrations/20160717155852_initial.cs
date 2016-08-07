using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SweetHome.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shelters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Address = table.Column<string>(maxLength: 160, nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    OwnerName = table.Column<string>(maxLength: 160, nullable: true),
                    URL = table.Column<string>(nullable: true),
                    VKGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShelterAnimals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    AnimalType = table.Column<int>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Color = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Info = table.Column<string>(maxLength: 500, nullable: true),
                    IsForFlat = table.Column<bool>(nullable: false),
                    IsForHome = table.Column<bool>(nullable: false),
                    IsHappy = table.Column<bool>(nullable: false),
                    IsHealthy = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OwnerName = table.Column<string>(nullable: true),
                    PlaceType = table.Column<int>(nullable: false),
                    ShelterId = table.Column<int>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Toilet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelterAnimals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShelterAnimals_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShelterAnimals_ShelterId",
                table: "ShelterAnimals",
                column: "ShelterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShelterAnimals");

            migrationBuilder.DropTable(
                name: "Shelters");
        }
    }
}
