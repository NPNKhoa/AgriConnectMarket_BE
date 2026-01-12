namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record TransactionDto(
        string TransactionRef,
        string BankCode,
        decimal Amount,
        string Status,
        DateTime CreatedAt
    );
}
