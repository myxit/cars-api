using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Antilopa.Migrations
{
    public partial class CreatedUpdatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Owners",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Owners",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Maintenance",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Maintenance",
                nullable: false,
                defaultValueSql: "NOW()");


            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedFrom",
                table: "Maintenance",
                nullable: true
                );

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedTo",
                table: "Maintenance",
                nullable: true
                );

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cars",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cars",
                nullable: false,
                defaultValueSql: "NOW()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Maintenance");

            migrationBuilder.DropColumn(
                name: "PlannedFrom",
                table: "Maintenance");

            migrationBuilder.DropColumn(
                name: "PlannedTo",
                table: "Maintenance");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Maintenance");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cars");
        }
    }
}
