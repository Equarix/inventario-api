using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    saleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<float>(type: "real", nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.saleId);
                    table.ForeignKey(
                        name: "FK_Sales_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    saleDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saleId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    priceSell = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => x.saleDetailId);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sales_saleId",
                        column: x => x.saleId,
                        principalTable: "Sales",
                        principalColumn: "saleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleMethods",
                columns: table => new
                {
                    saleMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    methodPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saleId = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    payMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleMethods", x => x.saleMethodId);
                    table.ForeignKey(
                        name: "FK_SaleMethods_Paymethods_payMethodId",
                        column: x => x.payMethodId,
                        principalTable: "Paymethods",
                        principalColumn: "paymethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleMethods_Sales_saleId",
                        column: x => x.saleId,
                        principalTable: "Sales",
                        principalColumn: "saleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_productId",
                table: "SaleDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_saleId",
                table: "SaleDetails",
                column: "saleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleMethods_payMethodId",
                table: "SaleMethods",
                column: "payMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleMethods_saleId",
                table: "SaleMethods",
                column: "saleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_clientId",
                table: "Sales",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_userId",
                table: "Sales",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "SaleMethods");

            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
