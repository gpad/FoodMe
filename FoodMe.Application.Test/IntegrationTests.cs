using System;
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

        [SetUp]
        public void Setup()
        {
            user = new User(UserId.New());
            cartRepository = new SqlCartRepository(ConnectionString);
            orderRepository = new SqlOrderRepository(ConnectionString);
            productsReadModel = new InMemoryProductsReadModel();
            orderReadModel = new InMemoryOrderReadModel();
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
        public void AddingProductToCartPopulateProductsReadModel()
        {
            var cart = Cart.CreateEmptyFor(user);
            cart.AddProduct(shampoo, 2);
            cart.AddProduct(soap, 1);

            cartRepository.SaveAsync(cart);

            Assert.That(() => productsReadModel.GetMostSeen(), Is.EqualTo(new[]{
                MostSeenProduct(shampoo, 2),
                MostSeenProduct(shampoo, 2),
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
