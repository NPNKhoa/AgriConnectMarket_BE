using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderAndFarmToHandleOrderPerFarm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("0e41ffc0-3a54-4cb3-bd60-23f87e37b473"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1b04557f-4ee1-4e7e-b75d-c4f027ada819"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("1fbbf776-b3b2-41bf-b72a-1cceeea4d404"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("36e104b9-493e-402e-ac9b-6767d510d720"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("52f526ed-489c-43e4-9148-d0bf0b63a174"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("9c3a7a32-ffe4-4295-aef7-5b534aa6bfe0"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("a2ff2525-460e-4e37-b94a-9a1fde74bfaa"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("b5a20a3c-8298-42b9-9962-9976401e467a"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("cd278d89-b3bf-449a-8ce3-9e31fe43b7fe"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("ec7475f7-d9eb-461f-aba4-48a6f8766079"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("fba20e3f-d97c-4c43-800b-6c8a93ada377"));

            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("032e8a45-13c0-41fc-8ef4-010b9bb197f8"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" },
                    { new Guid("0e70b57d-27f1-44a5-b165-97f34c249e0d"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("2ebd6320-c892-4f35-aab3-5208dd5a9ea7"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("4f72e132-f66d-4b42-8289-a09bc4025119"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("632ee97a-4b74-41f5-bde2-431517c141d2"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("9177b845-1a46-4bd6-92f8-bb9839c74535"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("91d4593b-645a-421e-be9b-7a40503456fd"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("aa30532f-5f14-4908-be14-b8b348a2939c"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("ed479144-ee8e-4ae5-9ee9-4133546aebfa"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("f9f43ca6-f8fe-4a4d-ae4f-d5001e9e1a46"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("fbcd39a4-3130-40c2-9d6c-5843c3067f44"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FarmId",
                table: "Orders",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Farms_FarmId",
                table: "Orders",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Farms_FarmId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FarmId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("032e8a45-13c0-41fc-8ef4-010b9bb197f8"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("0e70b57d-27f1-44a5-b165-97f34c249e0d"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2ebd6320-c892-4f35-aab3-5208dd5a9ea7"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4f72e132-f66d-4b42-8289-a09bc4025119"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("632ee97a-4b74-41f5-bde2-431517c141d2"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("9177b845-1a46-4bd6-92f8-bb9839c74535"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("91d4593b-645a-421e-be9b-7a40503456fd"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("aa30532f-5f14-4908-be14-b8b348a2939c"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("ed479144-ee8e-4ae5-9ee9-4133546aebfa"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("f9f43ca6-f8fe-4a4d-ae4f-d5001e9e1a46"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("fbcd39a4-3130-40c2-9d6c-5843c3067f44"));

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("0e41ffc0-3a54-4cb3-bd60-23f87e37b473"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("1b04557f-4ee1-4e7e-b75d-c4f027ada819"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("1fbbf776-b3b2-41bf-b72a-1cceeea4d404"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("36e104b9-493e-402e-ac9b-6767d510d720"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("52f526ed-489c-43e4-9148-d0bf0b63a174"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("9c3a7a32-ffe4-4295-aef7-5b534aa6bfe0"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("a2ff2525-460e-4e37-b94a-9a1fde74bfaa"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("b5a20a3c-8298-42b9-9962-9976401e467a"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" },
                    { new Guid("cd278d89-b3bf-449a-8ce3-9e31fe43b7fe"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("ec7475f7-d9eb-461f-aba4-48a6f8766079"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("fba20e3f-d97c-4c43-800b-6c8a93ada377"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" }
                });
        }
    }
}
