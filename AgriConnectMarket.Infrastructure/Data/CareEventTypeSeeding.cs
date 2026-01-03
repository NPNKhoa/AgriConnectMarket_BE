using AgriConnectMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Data
{
    public static class CareEventTypeSeeding
    {
        public static void ExecuteSeeding(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CareEventType>().HasData(
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Soil preparation",
                    "Prepare the soil before planting.",
                    "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Soil testing",
                    "Analyze soil samples and record results.",
                    "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Planting / transplanting",
                    "Record the planting or transplanting process.",
                    "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Irrigation",
                    "Provide water to crops.",
                    "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Fertilization",
                    "Provide nutrients to the crop.",
                    "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Pest and disease control",
                    "Manage pests or diseases using biological or chemical methods.",
                    "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Weeding",
                    "Remove weeds to reduce competition.",
                    "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Pruning / training",
                    "Adjust canopy, branches, or fruits to optimize growth.",
                    "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Growth monitoring",
                    "Record plant growth and identify risks early.",
                    "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Pollination",
                    "Support or record pollination activities.",
                    "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]"
                ),
                new CareEventType
                (
                    Guid.NewGuid(),
                    "Harvest",
                    "Record harvest timing and quantities.",
                    "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]"
                )
            );
        }

    }
}
