namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record AddViolationReportResponseDto(string CustomerName, string Content, string EvidenceUrl, Guid farmId, DateTime CreatedAt);
}
