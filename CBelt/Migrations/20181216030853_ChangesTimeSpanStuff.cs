using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBelt.Migrations
{
    public partial class ChangesTimeSpanStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "events",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "events",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "events",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "events",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
