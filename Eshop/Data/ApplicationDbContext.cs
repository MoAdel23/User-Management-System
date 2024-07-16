using Eshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        // Configuratiion using Fluent Api 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Add default schema for db
            // builder.HasDefaultSchema(schema: ""); 

            // igoner or delete column from table
            //builder.Entity<ApplicationUser>()
            //    .Ignore(u => u.EmailConfirmed);

            // Change Table names and Add Schema
            builder.Entity<ApplicationUser>()
                .ToTable(name:"Users" ,schema:"security");
            
            builder.Entity<IdentityRole>()
                .ToTable(name: "Roles", schema: "security");
           
            builder.Entity<IdentityUserRole<string>>()
                .ToTable(name: "UserRoles", schema: "security");
            
            builder.Entity<IdentityUserLogin<string>>()
                .ToTable(name: "UserLogins", schema: "security");
            
            builder.Entity<IdentityUserClaim<string>>()
                .ToTable(name: "UserClaims", schema: "security");
            
            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable(name: "RoleClaims", schema: "security");
           
            builder.Entity<IdentityUserToken<string>>()
                .ToTable(name: "UserTokens", schema: "security");

        }
    }
}
