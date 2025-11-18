using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToProductBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatchs_Seasons_SeasonId",
                table: "ProductBatchs");

            migrationBuilder.RenameTable(
                name: "ProductBatchs",
                newName: "ProductBatches");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBatchs_SeasonId",
                table: "ProductBatches",
                newName: "IX_ProductBatches_SeasonId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalYield",
                table: "ProductBatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableQuantity",
                table: "ProductBatches",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductBatches",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatches_Seasons_SeasonId",
                table: "ProductBatches",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatches_Seasons_SeasonId",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductBatches");

            migrationBuilder.RenameTable(
                name: "ProductBatches",
                newName: "ProductBatchs");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBatches_SeasonId",
                table: "ProductBatchs",
                newName: "IX_ProductBatchs_SeasonId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalYield",
                table: "ProductBatchs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableQuantity",
                table: "ProductBatchs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatchs_Seasons_SeasonId",
                table: "ProductBatchs",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }
    }
}
