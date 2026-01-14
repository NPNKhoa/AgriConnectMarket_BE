using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSellingPropertyToBatchEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsSelling",
                table: "ProductBatches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("1e25268f-5be0-4cc7-86bd-0e19e9801506"), "Record harvest timing and quantities.", "Harvest", "[\"Time (hour)\",\"Quantity (kg)\",\"Grade\",\"Worker team\",\"Bath code\",\"Destination\",\"Notes\"]" },
                    { new Guid("2426655e-248f-413e-bbb5-f95a16a48fa3"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("3c5e397b-7571-4750-bf5e-3e6386bcc164"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth (cm)\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("442cf1cc-0903-440e-8a2a-cbd04e78c384"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("51954882-48ef-4dec-bcd6-bbf48e40cc31"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated (ha)\",\"Labor\",\"Weed pressure (%)\",\"Notes\"]" },
                    { new Guid("61754173-efba-4aeb-91fa-409b0d1535e4"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density (%)\",\"Estimated fruit set (%)\",\"Notes\"]" },
                    { new Guid("65ba39c6-510c-4f10-b2bd-834363690adc"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\"]" },
                    { new Guid("a7746933-711b-44df-8fd9-bf73342a1966"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate (%)\",\"PHI (pre-harvest interval) - (days)\",\"REI (re-entry interval) - (days)\",\"Application equipment\",\"Weather during application\",\"Notes\"]" },
                    { new Guid("d1ba00f4-86c2-4c1f-9db3-0c58df90d247"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration (min)\",\"Water volume (l)\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("da76b141-29cb-4123-80dc-ab62c5599e63"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("da780ddc-ce92-4556-9ab5-029d3c2bbb59"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density (plant/cm)\",\"Planting method\",\"Germination rate (%)\",\"Notes\"]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1e25268f-5be0-4cc7-86bd-0e19e9801506"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2426655e-248f-413e-bbb5-f95a16a48fa3"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("3c5e397b-7571-4750-bf5e-3e6386bcc164"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("442cf1cc-0903-440e-8a2a-cbd04e78c384"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("51954882-48ef-4dec-bcd6-bbf48e40cc31"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("61754173-efba-4aeb-91fa-409b0d1535e4"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("65ba39c6-510c-4f10-b2bd-834363690adc"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("a7746933-711b-44df-8fd9-bf73342a1966"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("d1ba00f4-86c2-4c1f-9db3-0c58df90d247"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("da76b141-29cb-4123-80dc-ab62c5599e63"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("da780ddc-ce92-4556-9ab5-029d3c2bbb59"));

            migrationBuilder.DropColumn(
                name: "IsSelling",
                table: "ProductBatches");

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
    }
}
