﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable IDE0161 
#pragma warning disable CA1861 
#pragma warning disable IDE0053 

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Useralwayshasrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_role_id",
                table: "users");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "users",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "roles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "permission_role",
                newName: "permission_role",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "permission",
                newName: "permission",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "outbox_messages",
                newName: "outbox_messages",
                newSchema: "public");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                schema: "public",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                schema: "public",
                table: "users",
                column: "role_id",
                principalSchema: "public",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_role_id",
                schema: "public",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "public",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "public",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "permission_role",
                schema: "public",
                newName: "permission_role");

            migrationBuilder.RenameTable(
                name: "permission",
                schema: "public",
                newName: "permission");

            migrationBuilder.RenameTable(
                name: "outbox_messages",
                schema: "public",
                newName: "outbox_messages");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");
        }
    }
}
