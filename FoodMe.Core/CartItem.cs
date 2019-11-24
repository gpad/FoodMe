namespace FoodMe.Core
{
    public class CartItem
    {
        private ProductId productId;
        private int quantity;

        public CartItem(ProductId productId, int quantity)
        {
            this.productId = productId;
            this.quantity = quantity;
        }
    }
}
