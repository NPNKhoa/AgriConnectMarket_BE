using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCareEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCodeSequences",
                table: "OrderCodeSequences");

            migrationBuilder.RenameTable(
                name: "OrderCodeSequences",
                newName: "BatchCodeSequences");

            migrationBuilder.RenameColumn(
                name: "BacthCode",
                table: "ProductBatches",
                newName: "BatchCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BatchCodeSequences",
                table: "BatchCodeSequences",
                column: "Prefix");

            migrationBuilder.CreateTable(
                name: "CareEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EventTypeId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrevHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EventId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareEvents_CareEventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "CareEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareEvents_ProductBatches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "ProductBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareEvents_BatchId",
                table: "CareEvents",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_CareEvents_EventTypeId",
                table: "CareEvents",
                column: "EventTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareEvents");

            migrationBuilder.DropTable(
                name: "CareEventTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BatchCodeSequences",
                table: "BatchCodeSequences");

            migrationBuilder.RenameTable(
                name: "BatchCodeSequences",
                newName: "OrderCodeSequences");

            migrationBuilder.RenameColumn(
                name: "BatchCode",
                table: "ProductBatches",
                newName: "BacthCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCodeSequences",
                table: "OrderCodeSequences",
                column: "Prefix");
        }
    }
}
