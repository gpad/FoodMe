using System.Collections.Generic;

namespace FoodMe.ReadModel
{
    public interface IProductsReadModel
    {
        IEnumerable<Product> GetMostSeen();
    }
}
