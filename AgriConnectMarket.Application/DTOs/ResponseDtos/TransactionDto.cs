namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public record TransactionDto(
        string TransactionRef,
        string TransactionNo,
        string BankCode,
        decimal Amount,
        string Status,
        DateTime CreatedAt
    );
}
