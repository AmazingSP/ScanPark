using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server.Migrations
{
    public partial class plates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "LicensePlates");

            migrationBuilder.CreateTable(
                name: "RegisteredPlates",
                columns: table => new
                {
                    RegisteredPlateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    PlateIdentifier = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredPlates", x => x.RegisteredPlateId);
                });

            migrationBuilder.CreateTable(
                name: "Occurrences",
                columns: table => new
                {
                    OccurrenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(nullable: true),
                    Dureation = table.Column<string>(nullable: true),
                    Entrance = table.Column<string>(nullable: true),
                    Exit = table.Column<string>(nullable: true),
                    LicensePlate = table.Column<int>(nullable: false),
                    Paied = table.Column<bool>(nullable: false),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occurrences", x => x.OccurrenceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredPlates");

            migrationBuilder.DropTable(
                name: "Occurrences");

            migrationBuilder.CreateTable(
                name: "LicensePlates",
                columns: table => new
                {
                    LicencePlateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueLicensePlate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlates", x => x.LicencePlateId);
                });
        }
    }
}
