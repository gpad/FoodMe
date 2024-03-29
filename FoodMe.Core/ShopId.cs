using System;

namespace FoodMe.Core
{
    public class ShopId
    {
        private const string IdAsStringPrefix = "Shop-";

        public Guid Id { get; private set; }

        private ShopId(Guid id)
        {
            Id = id;
        }

        public ShopId(string id)
        {
            Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        public override string ToString()
        {
            return IdAsString();
        }

        public override bool Equals(object obj)
        {
            return obj is ShopId && Equals(Id, ((ShopId)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static ShopId New()
        {
            return new ShopId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{Id.ToString()}";
        }
    }
}
