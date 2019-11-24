using System;

namespace FoodMe.Core
{
    public class CartItem
    {
        public Guid Id {get;}
        public ProductId ProductId { get; }
        public int Quantity { get; }
        public string Description { get; }

        public CartItem(Guid Id, ProductId productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
