using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class Cart : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }

        public Profile Customer { get; set; }
        public IEnumerable<CartItem>? CartItems { get; set; }

        public Cart()
        {

        }

        private Cart(Guid customerId)
        {
            CustomerId = customerId;
            TotalPrice = 0;
        }

        public static Cart InitCart(Guid customerId) => new Cart(customerId);

        public void UpdateTotalPrice(decimal totalPrice) => TotalPrice = totalPrice;
    }
}
