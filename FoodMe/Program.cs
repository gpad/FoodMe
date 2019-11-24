using System;
using System.Threading.Tasks;
using FoodMe.Application.Infrastructure;
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
            var domainEventPubSub = new DomainEventPubSub();
            using (var policies = StartupPolicies())
            {
                if (args.Length == 1 && args[0] == "createCart")
                {
                    var cartService = CreateCartService(ConnectionString, domainEventPubSub);
                    var cart = await cartService.CreateAsync();
                    Console.WriteLine($"Created new cart {cart.Id}");
                    return 0;
                }
                if (args.Length == 4 && args[0] == "addProduct")
                {
                    var cartService = CreateCartService(ConnectionString, domainEventPubSub);
                    var cartId = args[1];
                    var productId = args[2];
                    var quantity = Int32.Parse(args[3]);
                    var cart = await cartService.GetAsync(cartId);
                    if (cart == null)
                    {
                        System.Console.WriteLine("Cart not Found!!!");
                        cart = await cartService.CreateAsync();
                    }
                    await cartService.AddProductAsync(cartId, productId, quantity);
                    Console.WriteLine($"Product added!!!");
                    return 0;
                }
                if (args.Length == 2 && args[0] == "checkout")
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
            return new ConsoleOrderService();
        }

        private static ICartService CreateCartService(string connectionString, IDomainEventPublisher publisher)
        {
            SqlCartRepository cartRepository = new SqlCartRepository(connectionString, publisher);
            return new ConsoleCartService(cartRepository);
        }
    }
}
