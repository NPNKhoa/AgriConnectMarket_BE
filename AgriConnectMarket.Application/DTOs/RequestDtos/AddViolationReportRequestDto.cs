namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record AddViolationReportRequestDto(Guid farmId, string content, string violationType, string? evidenceUrl);
}
