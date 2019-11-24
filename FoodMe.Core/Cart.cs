using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodMe.Core
{
    public class Cart : Aggregate<CartId>
    {
        private readonly List<CartItem> cartItems = new List<CartItem>();

        public IEnumerable<CartItem> Items => cartItems;

        public ShopId ShopId { get; }

        public static Cart CreateEmptyFor(User user)
        {
            return new Cart(CartId.New(), ShopId.New());
        }

        protected Cart(CartId id, ShopId shopId)
        {
            this.Id = id;
            this.ShopId = shopId;
        }

        protected Cart(IEnumerable<DomainEvent<CartId>> events)
        {
            Apply(events);
        }

        public Guid AddProduct(Product product, int quantity)
        {
            Guid itemId = Guid.NewGuid();
            Emit(ProductAdded.For(this, itemId, product, quantity));
            return itemId;
        }

        public void When(ProductAdded productAdded)
        {
            this.cartItems.Add(
                new CartItem(
                    productAdded.ItemId,
                    productAdded.ProductId,
                    "",
                    productAdded.Quantity));
        }

        public static Cart FromEvents(IEnumerable<DomainEvent<CartId>> events)
        {
            return new Cart(events);
        }
    }
}
