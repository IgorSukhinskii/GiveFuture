using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SweetHome.Migrations
{
    public partial class removeinfosizelimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShelterAnimals_Shelters_ShelterId",
                table: "ShelterAnimals");

            migrationBuilder.AlterColumn<int>(
                name: "ShelterId",
                table: "ShelterAnimals",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "ShelterAnimals",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShelterAnimals_Shelters_ShelterId",
                table: "ShelterAnimals",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShelterAnimals_Shelters_ShelterId",
                table: "ShelterAnimals");

            migrationBuilder.AlterColumn<int>(
                name: "ShelterId",
                table: "ShelterAnimals",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "ShelterAnimals",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShelterAnimals_Shelters_ShelterId",
                table: "ShelterAnimals",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
