using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSeasonRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Seasons_SeasonId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SeasonId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Seasons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_ProductId",
                table: "Seasons",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Products_ProductId",
                table: "Seasons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Products_ProductId",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_ProductId",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Seasons");

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_SeasonId",
                table: "Products",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Seasons_SeasonId",
                table: "Products",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
