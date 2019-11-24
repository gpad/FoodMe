namespace FoodMe.Application.Services
{
    public interface IOrderService
    {
        void Checkout(string cartId);
    }
}
