using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRegistryApp.Migrations
{
    /// <inheritdoc />
    public partial class CascadeToRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Categories_CategoryID",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Categories_CategoryID",
                table: "Pets",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Categories_CategoryID",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Categories_CategoryID",
                table: "Pets",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
