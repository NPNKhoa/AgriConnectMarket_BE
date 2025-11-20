using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class CartItem : BaseEntity<Guid>
    {
        public Guid CartId { get; set; }
        public Guid BatchId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }

        public Cart Cart { get; set; }
        public ProductBatch Batch { get; set; }

        public CartItem()
        {

        }

        private CartItem(Guid cartId, Guid itemId, int quantity, decimal itemPrice)
        {
            CartId = cartId;
            BatchId = itemId;
            Quantity = quantity;
            ItemPrice = itemPrice;
        }

        public static CartItem Create(Guid cartId, Guid itemId, int quantity, decimal itemPrice)
            => new CartItem(cartId, itemId, quantity, itemPrice);
    }
}
