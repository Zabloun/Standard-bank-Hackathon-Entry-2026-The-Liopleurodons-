using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Liopleurodons_Pocket_Business_Helper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
