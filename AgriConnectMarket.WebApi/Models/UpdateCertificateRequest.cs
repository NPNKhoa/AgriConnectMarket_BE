namespace AgriConnectMarket.WebApi.Models
{
    public class UpdateCertificateRequest
    {
        public Guid FarmId { get; set; }
        public IFormFile Certificate { get; set; }
    }
}
