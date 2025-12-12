namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record AddViolationReportResponseDto(string CustomerName, string Content, string EvidenceUrl, DateTime CreatedAt);
}
