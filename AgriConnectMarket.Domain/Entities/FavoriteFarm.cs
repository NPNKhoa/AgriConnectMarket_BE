using AgriConnectMarket.SharedKernel.Entities;

namespace AgriConnectMarket.Domain.Entities
{
    public class FavoriteFarm : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public Guid FarmId { get; set; }

        public Profile Customer { get; set; }
        public Farm Farm { get; set; }

        public FavoriteFarm()
        {

        }

        private FavoriteFarm(Guid customerId, Guid farmId)
        {
            CustomerId = customerId;
            FarmId = farmId;
        }

        public static FavoriteFarm Create(Guid customerId, Guid farmId) => new FavoriteFarm(customerId, farmId);
    }
}
