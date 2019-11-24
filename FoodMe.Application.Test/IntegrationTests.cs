using System;
using FoodMe.Core;
using NUnit.Framework;

namespace FoodMe.Application.Test
{
    public class IntegrationTests
    {
        private User user;
        private Product shampoo;
        private Product soap;
        private ICartRepository cartRepository;
        private ReadModel.IProductsReadModel productsReadModel;
        private IOrderRepository orderRepository;
        private ReadModel.IOrderReadModel orderReadModel;

        [SetUp]
        public void Setup()
        {
            user = new User(UserId.New());
        }

        private FoodMe.ReadModel.Product MostSeenProduct(Product shampo, int v)
        {
            throw new NotImplementedException();
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

            cartRepository.Save(cart);

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
            cartRepository.Save(cart);

            var order = Order.Checkout(cart);
            orderRepository.Save(order);

            var orders = orderReadModel.GetAllFor(cart.ShopId);

            Assert.That(orders, Is.EqualTo(new[] { ReadModelOrderFrom(order) }));
        }

    }
}
