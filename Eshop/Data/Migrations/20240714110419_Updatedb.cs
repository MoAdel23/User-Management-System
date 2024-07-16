using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "security",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "security",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }
    }
}
