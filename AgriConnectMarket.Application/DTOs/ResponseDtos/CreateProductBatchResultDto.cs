using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Domain.ValueObjects;

namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CreateProductBatchResultDto
    {
        public Guid BatchId { get; set; }
        public BatchCode BatchCode { get; set; }
        public decimal TotalYield { get; set; }
        public decimal AvailableQuantity { get; set; }
        public string Units { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }

        // Navigation
        public Guid SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
