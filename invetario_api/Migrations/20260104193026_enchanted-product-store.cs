using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class enchantedproductstore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "actualStock",
                table: "Product_Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "availableStock",
                table: "Product_Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "avgCost",
                table: "Product_Store",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Product_Store",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "lastCost",
                table: "Product_Store",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "maxStock",
                table: "Product_Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "minStock",
                table: "Product_Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "reservedStock",
                table: "Product_Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Product_Store",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actualStock",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "availableStock",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "avgCost",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "lastCost",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "maxStock",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "minStock",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "reservedStock",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Product_Store");
        }
    }
}
