namespace AgriConnectMarket.WebApi.Models
{
    public class CreateCareEventRequest
    {
        public Guid BatchId { get; set; }
        public Guid EventTypeId { get; set; }
        public string Payload { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
