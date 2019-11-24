using System.Collections.Generic;
using FoodMe.Core;

namespace FoodMe.ReadModel
{
    public interface IOrderReadModel
    {
        IEnumerable<Order> GetAllFor(ShopId shopId);
    }
}
