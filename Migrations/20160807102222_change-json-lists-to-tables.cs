using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SweetHome.Migrations
{
    public partial class changejsonliststotables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesSerialized",
                table: "ShelterAnimals");

            migrationBuilder.DropColumn(
                name: "PhonesSerialized",
                table: "ShelterAnimals");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "PhonesSerialized",
                table: "Shelters");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    AnimalId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ShelterAnimals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "ShelterAnimals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    AnimalId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    ShelterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_ShelterAnimals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "ShelterAnimals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phones_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Shelters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimalId",
                table: "Images",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_AnimalId",
                table: "Phones",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ShelterId",
                table: "Phones",
                column: "ShelterId");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Shelters",
                newName: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Shelters");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.AddColumn<string>(
                name: "ImagesSerialized",
                table: "ShelterAnimals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhonesSerialized",
                table: "ShelterAnimals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Shelters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhonesSerialized",
                table: "Shelters",
                nullable: true);

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Shelters",
                newName: "URL");
        }
    }
}
