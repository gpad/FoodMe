using System;
using System.Threading.Tasks;

namespace FoodMe.Core
{
    public interface IDomainEventSubscriber
    {
        void Subscribe<T>(Action<T> handler);

        void Subscribe<T>(Func<T, Task> handler);
    }
}
