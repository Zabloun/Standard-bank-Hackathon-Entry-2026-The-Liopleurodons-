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

            // Ensure required categories exist and get their IDs (handles existing DB state)
            // This way we can safely run the seed method multiple times without creating duplicates or relying on hardcoded IDs.
            // The hardcoded ID's were giving issues when running the seed method multiple times, because the IDs would change and the products would not be able to reference the correct categories.
            // Changed the EnsureCategory method to return the ID of the category, so that we can use it when adding products.
            int foodId = EnsureCategory(context, "FoodProducts", "Staple and packaged food items (flour, sugar, etc.)");
            int cleaningId = EnsureCategory(context, "CleaningProducts", "Household cleaning liquids, detergents and soaps");
            int toiletriesId = EnsureCategory(context, "Toiletries", "Personal care items like toothpaste and toilet paper");
            int stimulantsId = EnsureCategory(context, "Stimulants", "Nicotine and similar stimulant products");
            int prepaidId = EnsureCategory(context, "PrepaidProducts", "Prepaid airtime and vouchers");

            if (!context.Products.Any())
            {
                //Some Populating of things, just some like basic products-
                context.Products.AddRange(
                    new Product
                    {
                        ProductName = "Flour 5Kg",
                        Description = "A bag of flour weighing 5 kilograms.",
                        Price = 65.99m,
                        CategoryID = foodId
                    },
                    new Product
                    {
                        ProductName = "Sunlight Liquid 750ml",
                        Description = "A 750ml bottle of Sunlight liquid dishsoap.",
                        Price = 37.99m,
                        CategoryID = cleaningId

                    },
                    new Product
                    {
                        ProductName = "Toilet Paper 12 Rolls",
                        Description = "A pack of 12 rolls of toilet paper.",
                        Price = 99.99m,
                        CategoryID = toiletriesId
                    },
                    new Product
                    {
                        ProductName = "Cigarettes Carton",
                        Description = "A pack of 10 cigarettes packs.",
                        Price = 499.99m,
                        CategoryID = stimulantsId
                    },
                    new Product
                    {
                        ProductName = "MTN R20 Airtime",
                        Description = "A prepaid airtime voucher worth R20 for MTN network.",
                        Price = 20.00m,
                        CategoryID = prepaidId
                    },
                    new Product
                    {
                        ProductName = "Sugar 2Kg",
                        Description = "A bag of sugar weighing 2 kilograms.",
                        Price = 52.99m,
                        CategoryID = foodId
                    },
                    new Product
                    {
                        ProductName = "Laundry Detergent 1.5L",
                        Description = "A 1.5L bottle of laundry detergent.",
                        Price = 79.99m,
                        CategoryID = cleaningId
                    },
                    new Product
                    {
                        ProductName = "Toothpaste 100ml",
                        Description = "A 100ml tube of toothpaste.",
                        Price = 45.99m,
                        CategoryID = toiletriesId
                    },
                    new Product
                    {
                        ProductName = "Zamalek Quart(750ml)",
                        Description = "A 750ml bottle of Carling Black Label",
                        Price = 21.00m,
                        CategoryID = stimulantsId
                    },
                    new Product
                    {
                        ProductName = "Vodacom R50 Airtime",
                        Description = "A prepaid airtime voucher worth R50 for Vodacom network.",
                        Price = 50.00m,
                        CategoryID = prepaidId
                    }
                );
            }

            context.SaveChanges();
        }

        private static int EnsureCategory(AppDbContext context, string name, string description)
        {
            var existing = context.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (existing != null)
            {
                return existing.CategoryId;
            }

            var newCat = new Category
            {
                CategoryName = name,
                CategoryDescription = description
            };

            context.Categories.Add(newCat);
            context.SaveChanges(); // persist so Product FK can reference the generated ID
            return newCat.CategoryId;
        }
    }
}
