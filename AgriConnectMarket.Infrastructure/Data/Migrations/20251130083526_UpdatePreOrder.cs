using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePreOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreOrders_Products_ProductId",
                table: "PreOrders");

            migrationBuilder.DropIndex(
                name: "IX_PreOrders_ProductId",
                table: "PreOrders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PreOrders",
                newName: "BatchId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedReleaseDate",
                table: "PreOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_BatchId",
                table: "PreOrders",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrders_ProductBatches_BatchId",
                table: "PreOrders",
                column: "BatchId",
                principalTable: "ProductBatches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreOrders_ProductBatches_BatchId",
                table: "PreOrders");

            migrationBuilder.DropIndex(
                name: "IX_PreOrders_BatchId",
                table: "PreOrders");

            migrationBuilder.RenameColumn(
                name: "BatchId",
                table: "PreOrders",
                newName: "ProductId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedReleaseDate",
                table: "PreOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_ProductId",
                table: "PreOrders",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrders_Products_ProductId",
                table: "PreOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
