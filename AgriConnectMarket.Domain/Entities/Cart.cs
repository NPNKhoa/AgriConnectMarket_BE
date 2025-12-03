using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class Cart : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }

        public Profile Customer { get; set; }

        private readonly List<CartItem> _cartItems = new();
        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

        public Cart()
        {

        }

        private Cart(Guid customerId)
        {
            CustomerId = customerId;
            TotalPrice = 0;
        }

        public static Cart InitCart(Guid customerId) => new Cart(customerId);

        public void UpdateCartItem(CartItem item, ProductBatch batch, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(ci => ci.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.Batch = batch;
                existingItem.Quantity = quantity;
            }

            this.TotalPrice = _cartItems.Sum(i => i.ItemPrice);
        }

        public void UpdateTotalPrice(decimal totalPrice) => TotalPrice = totalPrice;
    }
}
