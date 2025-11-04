using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UPdateFarmEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FarmDesc",
                table: "Farms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Farms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmDesc",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Farms");
        }
    }
}
