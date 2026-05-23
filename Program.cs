using Liopleurodons_Pocket_Business_Helper.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Liopleurodons_Pocket_Business_Helper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            //Database Option 1: SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Database Option 2: SQLite 
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();

            // The UseAuthentication and UseAuthorization middleware are added
            // to the request pipeline to allow use of Identity features such as user authentication and authorization in the application.
            // This goes hanf in hand with the IdentityUser and IdentityRole services that were added to the service container earlier in the code.
            app.UseAuthentication();
            app.UseAuthorization();//order is important

            // least specific route - 0 required segments 
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

            SeedData.EnsurePopulated(app);

            app.Run();

            //If you get an error about something already existing in the database,
            //you can drop the database and re-run the application to re-create the database with the seed data.
            //don't forget that the migrations might need to be re-created as well.
            //You can do this by deleting the Migrations folder and running the following commands in the Package Manager Console:
            //Add-Migration InitialCreate and then Update-Database.
        }
    }
}
