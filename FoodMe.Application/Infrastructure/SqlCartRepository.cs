using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FoodMe.Core;
using Newtonsoft.Json;

namespace FoodMe.Application.Infrastructure
{
    public class SqlCartRepository : ICartRepository
    {
        private string connectionString;
        private readonly IDomainEventPublisher publisher;

        public SqlCartRepository(string connectionString, IDomainEventPublisher publisher)
        {
            this.connectionString = connectionString;
            this.publisher = publisher;
        }

        public async Task SaveAsync(Cart cart)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                using (var transaction = sqlConnection.BeginTransaction(IsolationLevel.Serializable))
                {
                    await sqlConnection.ExecuteAsync(
                        "insert carts (id) values (@Id)", new { Id = cart.Id.Id },
                        transaction: transaction);
                    await sqlConnection.ExecuteAsync(
                        "insert cart_items (id, product_id, description, qty) values (@Id, @ProductId, @Description, @Quantity)",
                        GetSqlCartItems(cart.Items),
                        transaction: transaction);
                    await sqlConnection.ExecuteAsync(
                        "insert events (id, aggregate_id, version, event_type, payload) values (@Id, @AggregateId, @Version, @EventType, @Payload)",
                        GetEventsFrom(cart.GetUncommittedEvents()),
                        transaction: transaction
                    );
                    await transaction.CommitAsync();
                    await PublisEvents(cart.GetUncommittedEvents());
                }
            }
        }

        private IEnumerable<dynamic> GetSqlCartItems(IEnumerable<CartItem> items)
        {
            return items.Select(item => new{
                Id = item.Id,
                ProductId = item.ProductId.Id,
                Description = "item.Description",
                Quantity = item.Quantity
            });
        }

        private async Task PublisEvents(IEnumerable<DomainEvent<CartId>> events)
        {
            foreach (var evt in events)
            {
                await publisher.PublishAsync((dynamic)evt);
            }
        }

        private IEnumerable<dynamic> GetEventsFrom(IEnumerable<DomainEvent<CartId>> events)
        {
            return events.Select(e => new
            {
                Id = e.EventId,
                AggregateId = e.AggregateId.Id,
                Version = e.AggregateVersion,
                EventType = e.GetType().ToString(),
                Payload = JsonConvert.SerializeObject(events)
            });
        }

        public Task<Cart> LoadAsync(CartId cartId)
        {
            throw new System.NotImplementedException();
        }
    }
}
