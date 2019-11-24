using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodMe.Core
{
    public class CartItem : IEquatable<CartItem>
    {
        public Guid Id { get; }
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as CartItem);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description, ProductId, Quantity);
        }

        public bool Equals([AllowNull] CartItem other)
        {
            return other != null
                && this.Id == other.Id
                && this.Description == other.Description
                && this.ProductId == other.ProductId
                && this.Quantity == other.Quantity;
        }

        public override string ToString()
        {
            return $"{nameof(CartItem)} {Id}, {Description}, {ProductId}, {Quantity}";
        }
    }
}
