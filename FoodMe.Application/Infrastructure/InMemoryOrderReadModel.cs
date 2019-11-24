using System.Collections.Generic;
using FoodMe.Core;

namespace FoodMe.Application.Infrastructure
{
    public class InMemoryOrderReadModel : ReadModel.IOrderReadModel
    {
        public IEnumerable<ReadModel.Order> GetAllFor(ShopId shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}
