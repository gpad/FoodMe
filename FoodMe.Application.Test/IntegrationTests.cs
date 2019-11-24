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
        private IProductsReadModel productsReadModel;
        private IOrderRepository orderRepository;
        private IOrderReadModel orderReadModel;

        [SetUp]
        public void Setup()
        {
            user = new User(UserId.New());
        }

        [Test]
        public void AddingProductToCartPopulateProductsReadModel()
        {
            var cart = Cart.CreateEmptyFor(user);
            cart.AddProduct(shampoo, 2);
            cart.AddProduct(soap, 1);

            cartRepository.Save(cart);

            Assert.That(() => productsReadModel.GetMostSeenProducts(), Is.EqualTo(new[]{
                MostSeenItem(shampoo, 2),
                MostSeenItem(shampoo, 2),
            }));
        }

        private object MostSeenItem(object shampo, int v)
        {
            throw new NotImplementedException();
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

            var orders = orderReadModel.GetAllOrderFor(cart.ShopId);

            Assert.That(orders, Is.EqualTo(new []{ReadModelOrderFrom(order)}));
        }

        private FoodMe.ReadModel.Order ReadModelOrderFrom(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
