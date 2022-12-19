using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class InitialMigration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Articul",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Articul1" ,
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Articul2",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name1",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name2",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Name1",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Name2",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "Articul",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
