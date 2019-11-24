using System;
using System.Collections.Generic;

namespace FoodMe.Core
{
    public class Cart
    {
        public ShopId ShopId { get;}

        public static Cart CreateEmptyFor(User user)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product shampoo, int v)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEvent> GetUncommittedEvents()
        {
            throw new NotImplementedException();
        }
    }
}
