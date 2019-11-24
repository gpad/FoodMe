using System.Threading.Tasks;

namespace FoodMe.Core
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
    }
}
