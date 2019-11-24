using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodMe.Core
{
    public class ProductAdded : DomainEvent<CartId>
    {
        public CartId CartId {get; }
        public ProductId ProductId {get;}
        public int Quantity {get;}

        private ProductAdded(CartId cartId, ProductId productId, int quantity, long aggregateVersion)
            :base(cartId, aggregateVersion)
        {
            this.CartId = cartId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public static ProductAdded For(Cart cart, Product product, int quantity)
        {
            return new ProductAdded(cart.Id, product.Id, quantity, cart.NextAggregateVersion + 1);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductAdded);
        }

        public override bool Equals([AllowNull] DomainEvent<CartId> other)
        {
            return Equals(other as ProductAdded);
        }

        public bool Equals(ProductAdded other)
        {
            return other != null
                && CartId.Equals(other.CartId)
                && ProductId.Equals(other.ProductId)
                && Quantity.Equals(other.Quantity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CartId, ProductId, Quantity);
        }
        public override string ToString()
        {
            return $"ProductAdded: {CartId} {ProductId} {Quantity}";
        }

    }
}
