using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coldel.Migrations
{
    public partial class NumberOfDaysreplacedwithCheckOutDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Registrations");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Registrations",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Registrations");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Registrations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
