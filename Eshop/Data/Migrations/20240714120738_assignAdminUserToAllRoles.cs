using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class assignAdminUserToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].UserRoles (UserId , RoleId) SELECT 'e60fea70-1216-4928-9e83-b7ce5aca6b66' , Id From [security].Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = 'e60fea70-1216-4928-9e83-b7ce5aca6b66';");
        }
    }
}
