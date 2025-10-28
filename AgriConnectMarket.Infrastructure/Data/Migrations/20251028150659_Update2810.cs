using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update2810 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FarmId",
                table: "Seasons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CertificateUrl",
                table: "Farms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_FarmId",
                table: "Seasons",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Farms_FarmId",
                table: "Seasons",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Farms_FarmId",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_FarmId",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "CertificateUrl",
                table: "Farms");
        }
    }
}
