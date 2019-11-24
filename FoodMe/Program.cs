using System;
using System.Threading.Tasks;
using FoodMe.Application.Services;
using FoodMe.Core;

namespace FoodMe
{
    class Program
    {
        private static readonly string ConnectionString = "Data Source=127.0.0.1,1433;Initial Catalog=FoodMeDev;User ID=SA;Password=StrongPassword1;";

        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MigrateDb();
            using (var policies = StartupPolicies())
            {

                if (args[0] == "addProduct")
                {
                    var cartService = CreateCartService();
                    var cartId = args[1];
                    var productId = args[2];
                    var quantity = Int32.Parse(args[3]);
                    var cart = await cartService.GetAsync(cartId);
                    if (cart == null)
                    {
                        cart = await cartService.CreateAsync(cartId);
                    }
                    await cartService.AddProductAsync(cartId, productId, quantity);
                    return 0;
                }
                if (args[0] == "checkout")
                {
                    var cartId = args[1];
                    var orderService = CreateOrderService();
                    orderService.Checkout(cartId);
                    return 0;
                }
                Console.WriteLine("No command to execute!!! Call:");
                Console.WriteLine("addProduct <cartId> <productId> <quantity>");
                Console.WriteLine("checkout <cartId>");
                return -1;
            }
        }

        private static void MigrateDb()
        {
            System.Console.WriteLine("Migrate the DB ...");
            Application.Migrations.Migrator.MigrateDb(ConnectionString);
            System.Console.WriteLine("Migration Completed!");
        }

        private static Policies StartupPolicies()
        {
            var policies = new Policies();
            policies.Add(new[]{
                new WarehouseUpdater()
            });
            policies.Start();
            return policies;
        }

        private static IOrderService CreateOrderService()
        {
            throw new NotImplementedException();
        }
        private static ICartService CreateCartService()
        {
            throw new NotImplementedException();
        }
    }
}
