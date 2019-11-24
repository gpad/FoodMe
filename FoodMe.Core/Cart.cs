using System;
using System.Collections.Generic;

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

        public void AddProduct(Product product, int quantity)
        {
            Emit(ProductAdded.For(this, product, quantity));
        }

        public void When(ProductAdded productAdded)
        {
            this.cartItems.Add(new CartItem(productAdded.ProductId, productAdded.Quantity));
        }

    }

    public class Aggregate<TAggregateId>
    {
        public const long NewAggregateVersion = -1;
        private long version = NewAggregateVersion;

        private readonly List<DomainEvent<TAggregateId>> uncommittedEvents = new List<DomainEvent<TAggregateId>>();

        public TAggregateId Id { get; protected set;}

        protected void Emit(DomainEvent<TAggregateId> @event)
        {
            this.uncommittedEvents.Add(@event);
            ((dynamic)this).When((dynamic)@event);
        }

        public IEnumerable<DomainEvent<TAggregateId>> GetUncommittedEvents()
        {
            return this.uncommittedEvents;
        }

    }


}
