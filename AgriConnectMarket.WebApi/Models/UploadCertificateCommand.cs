namespace AgriConnectMarket.WebApi.Models
{
    public class UploadCertificateCommand
    {
        public Guid FarmId { get; set; }
        public IFormFile Certificate { get; set; }
    }
}
