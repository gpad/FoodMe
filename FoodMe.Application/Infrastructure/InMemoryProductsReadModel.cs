using System.Collections.Generic;
using System.Linq;
using FoodMe.Core;

namespace FoodMe.Application.Infrastructure
{
    public class InMemoryProductsReadModel : ReadModel.IProductsReadModel
    {
        private readonly IDomainEventSubscriber subscriber;
        private readonly Dictionary<ProductId, ReadModel.Product> mostSeenProducts = new Dictionary<ProductId, ReadModel.Product>();

        public InMemoryProductsReadModel(IDomainEventSubscriber subscriber)
        {
            this.subscriber = subscriber;
            this.subscriber.Subscribe<ProductAdded>(HandleProductAddedEvent);
        }

        private void HandleProductAddedEvent(ProductAdded productAdded)
        {
            var entry = this.mostSeenProducts.GetValueOrDefault(productAdded.ProductId);
            entry = entry != null ? entry.Add(productAdded.Quantity) : new ReadModel.Product(productAdded.ProductId, "", 0.0M, productAdded.Quantity);
            this.mostSeenProducts[productAdded.ProductId] = entry;
        }

        public IEnumerable<ReadModel.Product> GetMostSeen()
        {
            return this.mostSeenProducts.Select(kv => kv.Value);
        }
    }

}
