using System.Threading.Tasks;

namespace FoodMe.Core
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync<T>(T publishedEvent);
    }
}
