namespace AgriConnectMarket.Infrastructure.Repositories
{
    public class CreatePaymentRequestDto
    {
        public IEnumerable<Guid> OrderId { get; set; }
    }
}
