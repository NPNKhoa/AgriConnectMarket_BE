using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionAndOrderEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "CareEventTypes",
                columns: new[] { "Id", "EventTypeDesc", "EventTypeName", "PayloadFields" },
                values: new object[,]
                {
                    { new Guid("083a550d-fb88-4016-850e-187fc2ba5cb4"), "Prepare the soil before planting.", "Soil preparation", "[\"Method\",\"Equipment used\",\"Tillage depth\",\"Number of passes\",\"Soil amendment (type)\",\"Soil amendment (amount)\",\"Fuel consumed\",\"Notes\"]" },
                    { new Guid("3bac39e7-fc9c-4aa1-b1a0-c869ddcf1b29"), "Record harvest timing and quantities.", "Harvest", "[\"Time\",\"Quantity\",\"Grade\",\"Worker team\",\"Post-harvest lot\",\"Destination\",\"Notes\"]" },
                    { new Guid("506260e8-e53b-4ec4-8f40-38742ec41b07"), "Manage pests or diseases using biological or chemical methods.", "Pest and disease control", "[\"Target pest/disease\",\"Product name\",\"Active ingredient\",\"Rate\",\"Dilution\",\"PHI (pre-harvest interval)\",\"REI (re-entry interval)\",\"Application equipment\",\"Weather during application\",\"PPE confirmation\",\"Notes\"]" },
                    { new Guid("70ef3d50-a3ae-4c44-99de-b3aebb35af64"), "Support or record pollination activities.", "Pollination", "[\"Pollination method\",\"Hive placement\",\"Bee density\",\"Estimated fruit set\",\"Notes\"]" },
                    { new Guid("83902528-c55b-495d-8c91-6277fb07ef79"), "Provide water to crops.", "Irrigation", "[\"Irrigation method\",\"Duration\",\"Water volume\",\"Water source\",\"Water treatment\",\"Weather notes\"]" },
                    { new Guid("ad45b5ce-45ad-4012-ba77-dba7c9df97cb"), "Adjust canopy, branches, or fruits to optimize growth.", "Pruning / training", "[\"Operation type\",\"Purpose\",\"Area / number of plants\",\"Waste handling\",\"Notes\"]" },
                    { new Guid("c4bec9c0-a6b8-4fbc-9b1d-0e2bbf981770"), "Remove weeds to reduce competition.", "Weeding", "[\"Method\",\"Area treated\",\"Labor\",\"Weed pressure\",\"Notes\"]" },
                    { new Guid("c4cec449-c829-4148-b43f-d686aef88696"), "Provide nutrients to the crop.", "Fertilization", "[\"Product name\",\"Formula\",\"Type (organic/synthetic)\",\"Rate\",\"Application method\",\"Withholding period\",\"Supplier\",\"Notes\"]" },
                    { new Guid("c9af872a-7ca8-47d4-aec0-c1ed636b9d66"), "Record plant growth and identify risks early.", "Growth monitoring", "[\"Observations\",\"Growth/height\",\"Pest or disease signs\",\"Photos\",\"Recommendations\",\"Follow-up tasks\"]" },
                    { new Guid("cfe61490-ffd2-49a8-b351-77ba3d8977fd"), "Record the planting or transplanting process.", "Planting / transplanting", "[\"Variety / seed lot\",\"Supplier\",\"Spacing / density\",\"Planting method\",\"Germination rate\",\"Notes\"]" },
                    { new Guid("e2ff1aff-be3e-4561-b713-a122d30b41b1"), "Analyze soil samples and record results.", "Soil testing", "[\"Sampling location\",\"Laboratory\",\"Parameters tested\",\"Results\",\"Recommendations\",\"Attachments\"]" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("083a550d-fb88-4016-850e-187fc2ba5cb4"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("3bac39e7-fc9c-4aa1-b1a0-c869ddcf1b29"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("506260e8-e53b-4ec4-8f40-38742ec41b07"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("70ef3d50-a3ae-4c44-99de-b3aebb35af64"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("83902528-c55b-495d-8c91-6277fb07ef79"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("ad45b5ce-45ad-4012-ba77-dba7c9df97cb"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("c4bec9c0-a6b8-4fbc-9b1d-0e2bbf981770"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("c4cec449-c829-4148-b43f-d686aef88696"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("c9af872a-7ca8-47d4-aec0-c1ed636b9d66"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("cfe61490-ffd2-49a8-b351-77ba3d8977fd"));

            migrationBuilder.DeleteData(
                table: "CareEventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e2ff1aff-be3e-4561-b713-a122d30b41b1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
