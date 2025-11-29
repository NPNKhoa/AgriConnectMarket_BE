namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class ItemGroupedByFarmDto
    {
        public Guid FarmId { get; set; }
        public string FarmName { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
    }
}
