namespace AgriConnectMarket.Application.DTOs.QueryDtos
{
    public class ProductQueryRequest
    {
        public string? searchTerm { get; set; } = "";
        public Guid? categoryId { get; set; } = null;
        public string? location { get; set; } = "";
    }
}
