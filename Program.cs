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

            // SQLite — works on Windows, Linux, and macOS without extra setup.
            // To switch to SQL Server, comment this out and uncomment the UseSqlServer block below,
            // then update appsettings.json with a valid SQL Server connection string.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // SQL Server option (requires a running SQL Server / LocalDB instance):
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            // UseAuthentication must come before UseAuthorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

            SeedData.EnsurePopulated(app);

            app.Run();
        }
    }
}
