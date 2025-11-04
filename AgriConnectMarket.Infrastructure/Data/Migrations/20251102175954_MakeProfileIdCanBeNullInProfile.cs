using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriConnectMarket.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeProfileIdCanBeNullInProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Profiles_ProfileId",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Profiles_ProfileId",
                table: "Addresses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Profiles_ProfileId",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Profiles_ProfileId",
                table: "Addresses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
