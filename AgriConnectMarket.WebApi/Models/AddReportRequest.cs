namespace AgriConnectMarket.WebApi.Models
{
    public record AddReportRequest(Guid farmId, string content, string violationType, IFormFile? evidenceImage);
}
