using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePitcure]) VALUES (N'e60fea70-1216-4928-9e83-b7ce5aca6b66', N'admin', N'ADMIN', N'admin@yahoo.com', N'ADMIN@YAHOO.COM', 0, N'AQAAAAIAAYagAAAAEGkpIXD6mmSYipA1TuzF3tOVJ8fUm0x1OdByfcRimDq30iGr6KfrSfIT6QN6YkSDpQ==', N'OSRGCZOREVMOE2MUSJDFJPVKXA5UW5UK', N'32f19b17-62d0-4187-ba09-01790273e508', NULL, 0, 0, NULL, 1, 0, N'Mohamed', N'Adel', NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = 'e60fea70-1216-4928-9e83-b7ce5aca6b66' ");
        }
    }
}
