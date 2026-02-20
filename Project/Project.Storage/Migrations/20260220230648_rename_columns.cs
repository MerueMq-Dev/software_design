using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Storage.Migrations
{
    /// <inheritdoc />
    public partial class rename_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "item",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "item",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "item",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "item",
                newName: "Id");
        }
    }
}
