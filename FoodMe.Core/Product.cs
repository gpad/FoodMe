namespace FoodMe.Core
{
    public class Product
    {
        public ProductId Id { get; }
        public string Name {get; }
        public decimal Price {get; }

        public Product(ProductId id, string name, decimal price)
        {
            this.Price = price;
            this.Name = name;
            this.Id = id;
        }
    }
}
