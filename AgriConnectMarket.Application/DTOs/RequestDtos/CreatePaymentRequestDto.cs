namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CreatePaymentRequestDto
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; } // VND
        public string OrderDescription { get; set; } = null!;
        public string ClientIp { get; set; } = "127.0.0.1";
    }
}
