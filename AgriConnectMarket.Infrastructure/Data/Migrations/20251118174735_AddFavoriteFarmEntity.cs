using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteFarmEntity : Migration
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

            migrationBuilder.CreateTable(
                name: "FavoriteFarms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FarmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FavoriteId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteFarms_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteFarms_Profiles_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteFarms_CustomerId",
                table: "FavoriteFarms",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteFarms_FarmId",
                table: "FavoriteFarms",
                column: "FarmId");

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

            migrationBuilder.DropTable(
                name: "FavoriteFarms");

            migrationBuilder.RenameTable(
                name: "ProductBatches",
                newName: "ProductBatchs");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBatches_SeasonId",
                table: "ProductBatchs",
                newName: "IX_ProductBatchs_SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatchs_Seasons_SeasonId",
                table: "ProductBatchs",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }
    }
}
