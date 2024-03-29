using System.Threading.Tasks;
using FoodMe.Core;

namespace FoodMe.Application.Services
{
    public interface ICartService
    {
        Task<Cart> GetAsync(string cartId);
        Task<Cart> CreateAsync();
        Task AddProductAsync(string cartId, string productId, int quantity);
    }
}
