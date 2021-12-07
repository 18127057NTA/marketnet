using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Suppliers.Any())
                {
                    // Do chạy từ Program.cs nên phải set đường link như vậy
                    var suppliersData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/suppliers.json");

                    var suppliers = JsonSerializer.Deserialize<List<Supplier>>(suppliersData);

                    foreach (var item in suppliers)
                    {
                        context.Suppliers.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Stores.Any())
                {
                    // Do chạy từ Program.cs nên phải set đường link như vậy
                    var storesData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/stores.json");

                    var stores = JsonSerializer.Deserialize<List<Store>>(storesData);

                    foreach (var item in stores)
                    {
                        context.Stores.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    // Do chạy từ Program.cs nên phải set đường link như vậy
                    var typesData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    // Do chạy từ Program.cs nên phải set đường link như vậy
                    var productsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}