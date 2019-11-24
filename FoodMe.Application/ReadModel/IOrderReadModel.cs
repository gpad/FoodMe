using FoodMe.Core;

namespace FoodMe.Application.Test
{
    public interface IOrderReadModel
    {
        object GetAllOrderFor(ShopId shopId);
    }
}
