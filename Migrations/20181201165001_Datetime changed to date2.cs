using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace coldel.Migrations
{
    public partial class Datetimechangedtodate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "Registrations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInDate",
                table: "Registrations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
