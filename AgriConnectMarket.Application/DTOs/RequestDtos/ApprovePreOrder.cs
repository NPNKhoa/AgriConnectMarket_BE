namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public record ApprovePreOrder(string bankName, string accountNumber, string Fullname, decimal depositAmount, DateTime expectedReleaseDate);
}
