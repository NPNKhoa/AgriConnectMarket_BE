namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class UploadCertificateDto
    {
        public Guid FarmId { get; set; }
        public string CertificateUrl { get; set; }
    }
}
