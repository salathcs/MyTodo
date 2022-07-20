using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class Basic_Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_identity_IdentityId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_IdentityId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_identity",
                table: "identity");

            migrationBuilder.RenameTable(
                name: "identity",
                newName: "identities");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "users",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "users",
                newName: "updated");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IdentityId",
                table: "users",
                newName: "identityId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "users",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "users",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "identities",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "identities",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "identities",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_identities",
                table: "identities",
                column: "id");

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permissionName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "todos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todos", x => x.id);
                    table.ForeignKey(
                        name: "FK_todos_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userPermissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<long>(type: "bigint", nullable: false),
                    permissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPermissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_userPermissions_permissions_permissionId",
                        column: x => x.permissionId,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userPermissions_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "created", "createdBy", "permissionName", "updated", "updatedBy" },
                values: new object[] { 1L, new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1544), "System", "AdminPermission", new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1544), "System" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1499), new DateTime(2022, 7, 20, 9, 34, 36, 592, DateTimeKind.Utc).AddTicks(1505) });

            migrationBuilder.InsertData(
                table: "userPermissions",
                columns: new[] { "id", "permissionId", "userId" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_users_identityId",
                table: "users",
                column: "identityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_identities_userName",
                table: "identities",
                column: "userName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_todos_userId",
                table: "todos",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userPermissions_permissionId",
                table: "userPermissions",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_userPermissions_userId",
                table: "userPermissions",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_identities_identityId",
                table: "users",
                column: "identityId",
                principalTable: "identities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_identities_identityId",
                table: "users");

            migrationBuilder.DropTable(
                name: "todos");

            migrationBuilder.DropTable(
                name: "userPermissions");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropIndex(
                name: "IX_users_identityId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_identities",
                table: "identities");

            migrationBuilder.DropIndex(
                name: "IX_identities_userName",
                table: "identities");

            migrationBuilder.RenameTable(
                name: "identities",
                newName: "identity");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated",
                table: "users",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "identityId",
                table: "users",
                newName: "IdentityId");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "users",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "identity",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "identity",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "identity",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_identity",
                table: "identity",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 7, 19, 20, 5, 34, 816, DateTimeKind.Utc).AddTicks(7066), new DateTime(2022, 7, 19, 20, 5, 34, 816, DateTimeKind.Utc).AddTicks(7071) });

            migrationBuilder.CreateIndex(
                name: "IX_users_IdentityId",
                table: "users",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_identity_IdentityId",
                table: "users",
                column: "IdentityId",
                principalTable: "identity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
