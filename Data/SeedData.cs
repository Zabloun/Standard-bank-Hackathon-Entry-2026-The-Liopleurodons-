using Liopleurodons_Pocket_Business_Helper.Models;
using Microsoft.EntityFrameworkCore;

namespace Liopleurodons_Pocket_Business_Helper.Data
{
    //Code taken from the GuitarShop example with some modifictaions
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Categories.Any())
            {
                //New product categoties, I think this covers all the things an informal buisness 
                //would sell, but feel free to add more if you think I am missing anything
                context.Categories.AddRange(
                    new Category { CategoryName = "FoodProducts" }, //ID = 1
                    new Category { CategoryName = "CleaningProducts" }, //ID = 2
                    new Category { CategoryName = "Toiletries" }, //ID = 3
                    new Category { CategoryName = "Stimulants" }, //ID =4
                    new Category { CategoryName = "PrepaidProducts" } //ID = 5
                    );
            }

            context.SaveChanges();

            if (!context.Products.Any())
            {
                //Some Populating of things, just some like basic products-
                context.Products.AddRange(
                    new Product
                    {
                        ProductName = "Flour 5Kg",
                        Description = "A bag of flour weighing 5 kilograms.",
                        Price = 65.99m,
                        CategoryID = 1
                    },
                    new Product
                    {
                        ProductName = "Sunlight Liquid 750ml",
                        Description = "A 750ml bottle of Sunlight liquid dishsoap.",
                        Price = 37.99m,
                        CategoryID = 2

                    },
                    new Product
                    {
                        ProductName = "Toilet Paper 12 Rolls",
                        Description = "A pack of 12 rolls of toilet paper.",
                        Price = 99.99m,
                        CategoryID = 3
                    },
                    new Product
                    {
                        ProductName = "Cigarettes Carton",
                        Description = "A pack of 10 cigarettes packs.",
                        Price = 499.99m,
                        CategoryID = 4
                    },
                    new Product
                    {
                        ProductName = "MTN R20 Airtime",
                        Description = "A prepaid airtime voucher worth R20 for MTN network.",
                        Price = 20.00m,
                        CategoryID = 5
                    },
                    new Product
                    {
                        ProductName = "Sugar 2Kg",
                        Description = "A bag of sugar weighing 2 kilograms.",
                        Price = 52.99m,
                        CategoryID = 1
                    },
                    new Product
                    {
                        ProductName = "Laundry Detergent 1.5L",
                        Description = "A 1.5L bottle of laundry detergent.",
                        Price = 79.99m,
                        CategoryID = 2
                    },
                    new Product
                    {
                        ProductName = "Toothpaste 100ml",
                        Description = "A 100ml tube of toothpaste.",
                        Price = 45.99m,
                        CategoryID = 3
                    },
                    new Product
                    {
                        ProductName = "Zamalek Quart(750ml)",
                        Description = "A 750ml bottle of Carling Black Label",
                        Price = 21.00m,
                        CategoryID = 4  
                    },
                    new Product
                    {
                        ProductName = "Vodacom R50 Airtime",
                        Description = "A prepaid airtime voucher worth R50 for Vodacom network.",
                        Price = 50.00m,
                        CategoryID = 5
                    }
                    );
            }

            context.SaveChanges();
        }
    }
}
