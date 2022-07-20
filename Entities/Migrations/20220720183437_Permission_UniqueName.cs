using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class Permission_UniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_permissions_permissionName",
                table: "permissions",
                column: "permissionName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_permissions_permissionName",
                table: "permissions");

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1544), new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1544) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1499), new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1505) });
        }
    }
}
