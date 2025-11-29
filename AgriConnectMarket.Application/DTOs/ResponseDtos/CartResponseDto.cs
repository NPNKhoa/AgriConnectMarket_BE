namespace AgriConnectMarket.Application.DTOs.ResponseDtos
{
    public class CartResponseDto
    {
        public Guid CartId { get; set; }
        public decimal TotalPrice { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<ItemGroupedByFarmDto> CartItems { get; set; } = new();
    }
}
