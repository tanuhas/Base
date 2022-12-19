using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class InitialMigration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Articul1",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Articul2",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Price1",
                table: "Contracts",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price2",
                table: "Contracts",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity1",
                table: "Contracts",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity2",
                table: "Contracts",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Articul1",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Articul2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Price1",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Price2",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Quantity1",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Quantity2",
                table: "Contracts");
        }
    }
}
