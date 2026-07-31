using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class box_sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "boxId",
                table: "Sales",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_boxId",
                table: "Sales",
                column: "boxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Boxs_boxId",
                table: "Sales",
                column: "boxId",
                principalTable: "Boxs",
                principalColumn: "boxId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Boxs_boxId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_boxId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "boxId",
                table: "Sales");
        }
    }
}
