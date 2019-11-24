using System;
using System.Threading.Tasks;
using FoodMe.Application.Services;
using FoodMe.Core;

namespace FoodMe
{
    internal class ConsoleCartService : ICartService
    {
        ICartRepository cartRepository;

        public ConsoleCartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task AddProductAsync(string cartId, string productId, int quantity)
        {
            var cart = await cartRepository.LoadAsync(new CartId(cartId));
            // var product = productRepository.LoadAsync(productId);
            // cart.AddProduct(product, quantity);
            await cartRepository.SaveAsync(cart);
        }

        public async Task<Cart> CreateAsync()
        {
            var cart = Cart.CreateEmptyFor(new User(UserId.New()));
            await cartRepository.SaveAsync(cart);
            return cart;
        }

        public Task<Cart> GetAsync(string cartId)
        {
            throw new NotImplementedException();
        }
    }
}
