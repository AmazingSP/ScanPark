using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class plates_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlateDistrict",
                table: "RegisteredPlates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlateNumber",
                table: "RegisteredPlates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlateDistrict",
                table: "RegisteredPlates");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                table: "RegisteredPlates");
        }
    }
}
