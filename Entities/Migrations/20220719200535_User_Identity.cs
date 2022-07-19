using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class User_Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IdentityId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_identity_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "identity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "identity",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { 1L, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "IdentityId", "Name", "Updated", "UpdatedBy" },
                values: new object[] { 1L, new DateTime(2022, 7, 19, 20, 5, 34, 816, DateTimeKind.Utc).AddTicks(7066), "System", "MyAdmin@tmp.com", 1L, "MyAdmin", new DateTime(2022, 7, 19, 20, 5, 34, 816, DateTimeKind.Utc).AddTicks(7071), "System" });

            migrationBuilder.CreateIndex(
                name: "IX_users_IdentityId",
                table: "users",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "identity");
        }
    }
}
