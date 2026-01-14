using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedingDataForCareEventType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1047aa07-f815-4be6-9121-6c8d2a901a6d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("10cead79-563b-4869-8c83-1a3e9c65372f"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("214cc956-4030-4df1-95a5-6bb52edfc18d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2751b065-8dbd-4f1c-a8ec-a754fd36d404"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("59cfeb7e-a3ab-4a1a-b742-65b85035d8e6"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("5d75825f-e47d-42c1-b33f-b25a47b1565d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("61f1919a-2935-413e-9ad6-abf586c699d6"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("6fb7b5a6-0eb7-4783-bc7f-4b42a3998ae1"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("82ccaa9c-0aa5-4696-8bea-d79aaebb69b0"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("957394df-ec3f-4bb6-a27e-a72679b8c8e2"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("d853fc35-519b-4c84-b8a6-a1d5b73668bd"));

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("126e6b2c-4cc1-4671-bf2b-420c51c19f48"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("17a7f52b-e4eb-4249-b2eb-53a7eb797ae5"), "Record harvest timing and quantities.", "Harvest", "[\"Time (hour)\",\"Quantity (kg)\",\"Grade\",\"Worker team\",\"Bath code\",\"Destination\",\"Notes\"]" },
                    { new Guid("4ebe9eb3-2b33-4548-b4a9-34fb3cdc3bb1"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density (%)\",\"Estimated fruit set (%)\",\"Notes\"]" },
                    { new Guid("520809b7-5def-42b8-8c8f-d8a997b8f524"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated (ha)\",\"Labor\",\"Weed pressure (%)\",\"Notes\"]" },
                    { new Guid("5c8929ed-9d25-4645-a310-b848ad434907"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth (cm)\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("75890bb2-3535-4d2e-b14f-17c20c1f954e"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("980fbe6f-7f2f-42f1-b0da-9ab67cd4dcd9"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density (plant/cm)\",\"Planting method\",\"Germination rate (%)\",\"Notes\"]" },
                    { new Guid("c503b93a-176e-4104-b788-17d31ddb4af2"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("e8f85c7e-34a6-4ecc-8045-709850b33925"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate (%)\",\"PHI (pre-harvest interval) - (days)\",\"REI (re-entry interval) - (days)\",\"Application equipment\",\"Weather during application\",\"Notes\"]" },
                    { new Guid("f3e0247b-9a1b-4ab6-8326-23fcbef76715"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\"]" },
                    { new Guid("f7c4c500-aa8b-41e2-8125-ce1e4b5eec56"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration (min)\",\"Water volume (l)\",\"Water source\",\"Water treatment\",\"Weather notes\"]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("126e6b2c-4cc1-4671-bf2b-420c51c19f48"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("17a7f52b-e4eb-4249-b2eb-53a7eb797ae5"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4ebe9eb3-2b33-4548-b4a9-34fb3cdc3bb1"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("520809b7-5def-42b8-8c8f-d8a997b8f524"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c8929ed-9d25-4645-a310-b848ad434907"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("75890bb2-3535-4d2e-b14f-17c20c1f954e"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("980fbe6f-7f2f-42f1-b0da-9ab67cd4dcd9"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("c503b93a-176e-4104-b788-17d31ddb4af2"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e8f85c7e-34a6-4ecc-8045-709850b33925"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("f3e0247b-9a1b-4ab6-8326-23fcbef76715"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("f7c4c500-aa8b-41e2-8125-ce1e4b5eec56"));

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("1047aa07-f815-4be6-9121-6c8d2a901a6d"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("10cead79-563b-4869-8c83-1a3e9c65372f"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("214cc956-4030-4df1-95a5-6bb52edfc18d"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("2751b065-8dbd-4f1c-a8ec-a754fd36d404"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("59cfeb7e-a3ab-4a1a-b742-65b85035d8e6"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("5d75825f-e47d-42c1-b33f-b25a47b1565d"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("61f1919a-2935-413e-9ad6-abf586c699d6"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("6fb7b5a6-0eb7-4783-bc7f-4b42a3998ae1"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("82ccaa9c-0aa5-4696-8bea-d79aaebb69b0"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("957394df-ec3f-4bb6-a27e-a72679b8c8e2"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" },
                    { new Guid("d853fc35-519b-4c84-b8a6-a1d5b73668bd"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" }
                });
        }
    }
}
