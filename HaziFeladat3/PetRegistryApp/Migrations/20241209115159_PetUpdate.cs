using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRegistryApp.Migrations
{
    /// <inheritdoc />
    public partial class PetUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ContractExp",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PetType",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Retired",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractExp",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetType",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Retired",
                table: "Pets");
        }
    }
}
