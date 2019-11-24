using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using FoodMe.Application.Infrastructure;
using FoodMe.Application.Migrations;
using FoodMe.Core;
using NUnit.Framework;

namespace FoodMe.Application.Test
{
    public class IntegrationTests
    {
        private static readonly string ConnectionString = "Data Source=127.0.0.1,1433;Initial Catalog=FoodMeDev;User ID=SA;Password=StrongPassword1;";
        private User user;
        private Product shampoo = new Product(ProductId.New(), "shampoo", 12.32M);
        private Product soap = new Product(ProductId.New(), "soap", 3.42M);
        private ICartRepository cartRepository;
        private IOrderRepository orderRepository;
        private ReadModel.IProductsReadModel productsReadModel;
        private ReadModel.IOrderReadModel orderReadModel;
        private DomainEventPubSub domainEventPubSub;

        [OneTimeSetUp]
        public void DataBaseFixtureOneTimeSetUp()
        {
            Migrator.MigrateDb(ConnectionString);
        }

        [SetUp]
        public void Setup()
        {
            DeleteTables("carts", "cart_items");
            user = new User(UserId.New());
            domainEventPubSub = new DomainEventPubSub();
            cartRepository = new SqlCartRepository(ConnectionString, domainEventPubSub);
            orderRepository = new SqlOrderRepository(ConnectionString, domainEventPubSub);
            productsReadModel = new InMemoryProductsReadModel(domainEventPubSub);
            orderReadModel = new InMemoryOrderReadModel();
        }

        [TearDown]
        public void TearDown()
        {
            domainEventPubSub.Dispose();
        }

        protected void DeleteTables(params string[] tables)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                tables.AsList().ForEach(table => sqlConnection.Execute($"DELETE from {table}"));
            }
        }

        private FoodMe.ReadModel.Product MostSeenProduct(Product product, int quantity)
        {
            return new FoodMe.ReadModel.Product(
                product.Id,
                product.Name,
                product.Price,
                quantity);
        }

        private FoodMe.ReadModel.Order ReadModelOrderFrom(Order order)
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task AddingProductToCartPopulateProductsReadModel()
        {
            var cart = Cart.CreateEmptyFor(user);
            cart.AddProduct(shampoo, 2);
            cart.AddProduct(soap, 1);

            await cartRepository.SaveAsync(cart);

            Assert.That(productsReadModel.GetMostSeen(), Is.EqualTo(new[]{
                MostSeenProduct(shampoo, 2),
                MostSeenProduct(soap, 1),
            }));
        }

        [Test]
        public void CheckOutCartPlaceOrderToProperShop()
        {
            var cart = Cart.CreateEmptyFor(user);
            cart.AddProduct(shampoo, 2);
            cart.AddProduct(soap, 1);
            cartRepository.SaveAsync(cart);

            var order = Order.Checkout(cart);
            orderRepository.SaveAsync(order);

            var orders = orderReadModel.GetAllFor(cart.ShopId);

            Assert.That(orders, Is.EqualTo(new[] { ReadModelOrderFrom(order) }));
        }

    }
}
