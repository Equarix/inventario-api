using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class removecolumnsstoreproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "availableStock",
                table: "Product_Store");

            migrationBuilder.DropColumn(
                name: "reservedStock",
                table: "Product_Store");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "availableStock",
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
        }
    }
}
