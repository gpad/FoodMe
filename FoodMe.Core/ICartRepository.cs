using System.Threading.Tasks;

namespace FoodMe.Core
{
    public interface ICartRepository
    {
        Task SaveAsync(Cart cart);
        Task<Cart> LoadAsync(CartId cartId);
    }
}
