namespace AgriConnectMarket.Application.DTOs.RequestDtos
{
    public class CreateProductBatchDto
    {
        public Guid SeasonId { get; set; }
        public decimal TotalYield { get; set; }
        public decimal AvailableQuantity { get; set; }
        public string Units { get; set; }
        public DateTime PlantingDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
    }
}
