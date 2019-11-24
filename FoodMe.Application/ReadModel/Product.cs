using System;
using System.Diagnostics.CodeAnalysis;
using FoodMe.Core;

namespace FoodMe.ReadModel
{
    public class Product : System.IEquatable<Product>
    {
        public Product(ProductId id, string name, decimal price, long quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public ProductId Id { get; }
        public string Name {get; }
        public decimal Price {get; }
        public long Quantity {get; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            return Equals (obj as Product);
        }

        internal Product Add(int quantity)
        {
            return new Product(Id, Name, Price, Quantity + quantity);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Id, Name, Price, Quantity);
        }

        public bool Equals([AllowNull] Product other)
        {
            return other != null
            && Id == other.Id
            && Name == other.Name
            && Price == other.Price
            && Quantity == other.Quantity;
        }

        public override string ToString()
        {
            return $"{nameof(Product)} {Id} {Name} {Price} {Quantity}";
        }
    }
}
