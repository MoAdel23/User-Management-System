using Eshop.Data;
using Eshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eshop;

public class Program
{
    // Article important : 
    // https://codewithmukesh.com/blog/permission-based-authorization-in-aspnet-core/#whats-role-based-authorization
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Registere DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Add Razor Runtime Compilation
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add Identity services
        // Registere ApplicationUser , IdentityRole
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>( )
            //options => options.SignIn.RequireConfirmedEmail = true) // Confirm Email
            // configures Identity to use Entity Framework Core as the data store and specifies ApplicationDbContext as the context class
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
