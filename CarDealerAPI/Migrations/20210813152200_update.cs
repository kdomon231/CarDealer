using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealerAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarList",
                table: "CarList");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarList");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "CarList",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarList",
                table: "CarList",
                column: "VIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarList",
                table: "CarList");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "CarList",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CarList",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarList",
                table: "CarList",
                column: "Id");
        }
    }
}
