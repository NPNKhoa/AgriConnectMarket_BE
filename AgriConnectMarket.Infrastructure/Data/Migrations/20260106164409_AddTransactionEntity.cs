using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1fd21bf3-206c-4fa1-a372-a61d92d680b9"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2da5776f-3c48-4c07-960e-175988480bc6"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("45a31a1a-4a2c-4182-88fe-b35935b8c154"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4a720d74-847b-40b5-b4d8-94eec5592a4a"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c44dc13-9787-482c-b1d5-3a92a5dee6f9"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("76d59d42-4c59-4ada-a87f-09f3d9153c15"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("7bd4fc45-8575-4135-b54c-9481b610111e"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("9db188d0-bd28-4389-adaa-2e8fe345e10e"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("b7e5e9f3-d8a9-4ff3-b9ab-46fc488e4214"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("d325d4fa-6b22-4118-90a5-2e36c08a7d00"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e282f70c-5934-41e7-9440-9b844d50a7b3"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("09633cb0-201a-4b88-a384-c5ca49e4f051"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("14203f16-7060-4d9c-907c-7ccdf88cd566"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("2b44460d-77a1-4814-8024-35cdfab57f70"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("2d57ddc3-94df-4e5a-ba71-69692c934ec3"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("2ef86933-294f-4af2-ac18-ef74ce456f35"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("309c6420-dfea-4c4f-bfd0-7d92625fbe6e"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("33a5dffb-b2e9-47fc-9edc-6edfcb5e36c6"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" },
                    { new Guid("597abe08-eca6-4362-b6e7-d1e07afcb3b6"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("780a5349-f522-4615-980b-74d53ccf040d"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("7efda1de-a5b6-402d-b621-b0e58cc970e3"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" },
                    { new Guid("8272b172-b746-41e9-ac05-d1dbdbaaa875"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderId1",
                table: "Transaction",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Orders_OrderId1",
                table: "Transaction",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Orders_OrderId1",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_OrderId1",
                table: "Transaction");

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("09633cb0-201a-4b88-a384-c5ca49e4f051"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("14203f16-7060-4d9c-907c-7ccdf88cd566"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2b44460d-77a1-4814-8024-35cdfab57f70"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2d57ddc3-94df-4e5a-ba71-69692c934ec3"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2ef86933-294f-4af2-ac18-ef74ce456f35"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("309c6420-dfea-4c4f-bfd0-7d92625fbe6e"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("33a5dffb-b2e9-47fc-9edc-6edfcb5e36c6"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("597abe08-eca6-4362-b6e7-d1e07afcb3b6"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("780a5349-f522-4615-980b-74d53ccf040d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("7efda1de-a5b6-402d-b621-b0e58cc970e3"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("8272b172-b746-41e9-ac05-d1dbdbaaa875"));

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Transaction");

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("1fd21bf3-206c-4fa1-a372-a61d92d680b9"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("2da5776f-3c48-4c07-960e-175988480bc6"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("45a31a1a-4a2c-4182-88fe-b35935b8c154"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" },
                    { new Guid("4a720d74-847b-40b5-b4d8-94eec5592a4a"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("5c44dc13-9787-482c-b1d5-3a92a5dee6f9"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("76d59d42-4c59-4ada-a87f-09f3d9153c15"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("7bd4fc45-8575-4135-b54c-9481b610111e"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" },
                    { new Guid("9db188d0-bd28-4389-adaa-2e8fe345e10e"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("b7e5e9f3-d8a9-4ff3-b9ab-46fc488e4214"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("d325d4fa-6b22-4118-90a5-2e36c08a7d00"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("e282f70c-5934-41e7-9440-9b844d50a7b3"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" }
                });
        }
    }
}
