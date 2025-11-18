namespace AgriConnectMarket.Infrastructure.Entities
{
    public class BatchCodeSequence
    {
        public string Prefix { get; set; } = null!;
        public int LastNumber { get; set; }
    }
}
