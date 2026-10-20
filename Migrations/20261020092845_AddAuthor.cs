using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ona_Mihai_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Category",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Category",
                newName: "CategoryID");
        }
    }
}
