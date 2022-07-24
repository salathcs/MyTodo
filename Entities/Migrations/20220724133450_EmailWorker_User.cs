using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class EmailWorker_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "identities",
                columns: new[] { "id", "password", "userName" },
                values: new object[] { 2L, "r1eH#emE295&", "emailWorker" });

            migrationBuilder.UpdateData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 24, 13, 34, 50, 122, DateTimeKind.Utc).AddTicks(2709), new DateTime(2022, 7, 24, 13, 34, 50, 122, DateTimeKind.Utc).AddTicks(2709) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 24, 13, 34, 50, 122, DateTimeKind.Utc).AddTicks(2229), new DateTime(2022, 7, 24, 13, 34, 50, 122, DateTimeKind.Utc).AddTicks(2232) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created", "createdBy", "email", "identityId", "name", "updated", "updatedBy" },
                values: new object[] { 2L, new DateTime(2022, 7, 24, 13, 34, 50, 122, DateTimeKind.Utc).AddTicks(2234), "System", "MyAdmin@tmp.com", 2L, "EmailWorker", new DateTime(2022, 7, 24, 13, 34, 50, 122, DateTimeKind.Utc).AddTicks(2235), "System" });

            migrationBuilder.InsertData(
                table: "userPermissions",
                columns: new[] { "id", "permissionId", "userId" },
                values: new object[] { 2L, 1L, 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "userPermissions",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "identities",
                keyColumn: "id",
                keyValue: 2L);

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
    }
}
