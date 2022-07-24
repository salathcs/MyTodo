using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class Todo_EmailSent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "emailSent",
                table: "todos",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 24, 12, 9, 23, 518, DateTimeKind.Utc).AddTicks(3285), new DateTime(2022, 7, 24, 12, 9, 23, 518, DateTimeKind.Utc).AddTicks(3286) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 24, 12, 9, 23, 518, DateTimeKind.Utc).AddTicks(2883), new DateTime(2022, 7, 24, 12, 9, 23, 518, DateTimeKind.Utc).AddTicks(2885) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailSent",
                table: "todos");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 20, 18, 34, 37, 51, DateTimeKind.Utc).AddTicks(5944), new DateTime(2022, 7, 20, 18, 34, 37, 51, DateTimeKind.Utc).AddTicks(5946) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 20, 18, 34, 37, 51, DateTimeKind.Utc).AddTicks(5554), new DateTime(2022, 7, 20, 18, 34, 37, 51, DateTimeKind.Utc).AddTicks(5556) });
        }
    }
}
