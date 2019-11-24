using System.Collections.Generic;

namespace FoodMe.Core
{
    public class InMemoryProductsReadModel : ReadModel.IProductsReadModel
    {
        public IEnumerable<ReadModel.Product> GetMostSeen()
        {
            throw new System.NotImplementedException();
        }
    }
}
