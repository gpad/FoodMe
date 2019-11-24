using System.Collections.Generic;

namespace FoodMe.Core
{
    public class InMemoryOrderReadModel : ReadModel.IOrderReadModel
    {
        public IEnumerable<ReadModel.Order> GetAllFor(ShopId shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}
