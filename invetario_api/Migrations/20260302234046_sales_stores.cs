using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class sales_stores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "storeId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_storeId",
                table: "Sales",
                column: "storeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Stores_storeId",
                table: "Sales",
                column: "storeId",
                principalTable: "Stores",
                principalColumn: "storeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Stores_storeId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_storeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "storeId",
                table: "Sales");
        }
    }
}
