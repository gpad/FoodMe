using NUnit.Framework;

namespace FoodMe.Core.Test
{
    public class CartTest
    {
        private User user;
        private Product shampoo = new Product(ProductId.New(), "shampoo", 12.32M);
        private Product soap = new Product(ProductId.New(), "soap", 3.42M);

        [SetUp]
        public void Setup()
        {
            user = new User(UserId.New());
        }

        [Test]
        public void StoringCartAggregateEmitEvent()
        {
            var cart = Cart.CreateEmptyFor(user);

            cart.AddProduct(shampoo, 1);
            cart.AddProduct(soap, 1);

            Assert.That(cart.GetUncommittedEvents(), Is.EqualTo(new[]{
                ProductAdded.For(cart, shampoo, 1),
                ProductAdded.For(cart, soap, 1)
            }));
        }

        [Test]
        public void AddProductToCartIncreaseCartItem()
        {
            var cart = Cart.CreateEmptyFor(user);
            cart.AddProduct(shampoo, 1);
            cart.AddProduct(soap, 1);

            Assert.That(cart.Items, Is.Not.Empty);
        }

    }
}
