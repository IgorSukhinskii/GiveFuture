using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SweetHome.Migrations
{
    public partial class fixserializedarrays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagesSerialized",
                table: "ShelterAnimals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhonesSerialized",
                table: "ShelterAnimals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhonesSerialized",
                table: "Shelters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesSerialized",
                table: "ShelterAnimals");

            migrationBuilder.DropColumn(
                name: "PhonesSerialized",
                table: "ShelterAnimals");

            migrationBuilder.DropColumn(
                name: "PhonesSerialized",
                table: "Shelters");
        }
    }
}
