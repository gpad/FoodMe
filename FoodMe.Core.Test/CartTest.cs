using NUnit.Framework;

namespace FoodMe.Core.Test
{
    public class CartTest
    {
        private User user;
        private Product shampoo;
        private Product soap;

        public DomainEvent ReceivedEvents { get; private set; }

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

            Assert.That(() => cart.GetUncommittedEvents(), Is.EqualTo(new[]{
                InsertedProductEventBuilder.For(shampoo).In(cart).Build(),
                InsertedProductEventBuilder.For(soap).In(cart).Build()
            }));
        }
    }
}
