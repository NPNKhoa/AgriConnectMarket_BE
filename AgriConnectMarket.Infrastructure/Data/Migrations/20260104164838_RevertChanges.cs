using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
