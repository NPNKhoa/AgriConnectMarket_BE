using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionAndOrderEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Orders_OrderId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Orders_OrderId1",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_OrderId",
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
                name: "OrderId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionNo",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("30e09f5d-64ce-4b70-9c3e-10750df64abf"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("4839440a-c172-4ee7-82af-8a9def81483c"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("5c698c20-8bc1-492b-b1df-e818244220bc"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" },
                    { new Guid("5e1ed9f8-1159-4b7b-bb48-1fe2286207c1"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("6868b89a-b3ab-41e3-adca-e990a633ae0e"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("958e8c28-4216-4a92-ae74-a212b6fddeca"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("987e94f2-61e5-480f-9e62-2db007a5558a"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("b0644382-10d1-43c4-b6da-0fade22a74a9"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" },
                    { new Guid("e0f28f12-8839-4baf-9353-a7ac74aec808"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("e2fd58a0-ca3b-4566-b392-bdf3fbfa2e2d"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("f36537b1-8451-4651-b27f-cd2e21bdac58"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TransactionId",
                table: "Orders",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Transaction_TransactionId",
                table: "Orders",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Transaction_TransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TransactionId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("30e09f5d-64ce-4b70-9c3e-10750df64abf"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4839440a-c172-4ee7-82af-8a9def81483c"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c698c20-8bc1-492b-b1df-e818244220bc"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("5e1ed9f8-1159-4b7b-bb48-1fe2286207c1"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("6868b89a-b3ab-41e3-adca-e990a633ae0e"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("958e8c28-4216-4a92-ae74-a212b6fddeca"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("987e94f2-61e5-480f-9e62-2db007a5558a"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("b0644382-10d1-43c4-b6da-0fade22a74a9"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e0f28f12-8839-4baf-9353-a7ac74aec808"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e2fd58a0-ca3b-4566-b392-bdf3fbfa2e2d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("f36537b1-8451-4651-b27f-cd2e21bdac58"));

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionNo",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "IX_Transaction_OrderId",
                table: "Transaction",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderId1",
                table: "Transaction",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Orders_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Orders_OrderId1",
                table: "Transaction",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
