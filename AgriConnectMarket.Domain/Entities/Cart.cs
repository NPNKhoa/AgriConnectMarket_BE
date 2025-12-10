using AgriConnectMarket.SharedKernel.Entities;
using AgriConnectMarket.SharedKernel.Guards;

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

        public CartItem UpdateCartItem(CartItem? item, Guid batchId, decimal batchPrice, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(ci => ci.Id == item.Id);

            if (item is null || existingItem is null)
            {
                var newItem = CartItem.Create(this.Id, batchId, quantity, batchPrice * quantity);
                _cartItems.Add(newItem);
                return newItem;
            }

            existingItem.Quantity = quantity;

            existingItem.ItemPrice = quantity * batchPrice;

            ReCalculateTotalPrice();

            return existingItem;
        }

        public void UpdateTotalPrice(decimal totalPrice) => TotalPrice = totalPrice;

        public void DeleteFromCart(CartItem item)
        {
            Guard.AgainstNull(item, nameof(item));

            _cartItems.Remove(item);

            ReCalculateTotalPrice();
        }

        public void DeleteAllFromCart()
        {
            _cartItems.Clear();
            ReCalculateTotalPrice();
        }

        private void ReCalculateTotalPrice(bool isDeleteAll = false)
        {
            if (isDeleteAll)
            {
                TotalPrice = 0;
                return;
            }

            TotalPrice = _cartItems.Sum(i => i.ItemPrice);
        }
    }
}
