﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable IDE0161 
#pragma warning disable CA1861 
#pragma warning disable IDE0053 

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AddedRoles : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "role_id",
            table: "users",
            type: "integer",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "roles",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_roles", x => x.id);
            });

        migrationBuilder.InsertData(
            table: "roles",
            columns: new[] { "id", "name" },
            values: new object[,]
            {
                { 1, "Member" },
                { 2, "Manager" },
                { 3, "Admin" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_users_role_id",
            table: "users",
            column: "role_id");

        migrationBuilder.AddForeignKey(
            name: "fk_users_roles_role_id",
            table: "users",
            column: "role_id",
            principalTable: "roles",
            principalColumn: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_users_roles_role_id",
            table: "users");

        migrationBuilder.DropTable(
            name: "roles");

        migrationBuilder.DropIndex(
            name: "ix_users_role_id",
            table: "users");

        migrationBuilder.DropColumn(
            name: "role_id",
            table: "users");
    }
}
