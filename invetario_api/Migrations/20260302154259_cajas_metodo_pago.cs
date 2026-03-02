using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class cajas_metodo_pago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "paymentMethodId",
                table: "Boxmoves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Boxmoves_paymentMethodId",
                table: "Boxmoves",
                column: "paymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxmoves_Paymethods_paymentMethodId",
                table: "Boxmoves",
                column: "paymentMethodId",
                principalTable: "Paymethods",
                principalColumn: "paymethodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxmoves_Paymethods_paymentMethodId",
                table: "Boxmoves");

            migrationBuilder.DropIndex(
                name: "IX_Boxmoves_paymentMethodId",
                table: "Boxmoves");

            migrationBuilder.DropColumn(
                name: "paymentMethodId",
                table: "Boxmoves");
        }
    }
}
