using System;

namespace FoodMe.Core
{
    public class CartItem
    {
        public Guid Id {get;}
        public ProductId ProductId { get; }
        public int Quantity { get; }
        public string Description { get; }

        public CartItem(Guid id, ProductId productId, string name, int quantity)
        {
            this.Id = id;
            this.Description = name;
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
