using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class departureOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departureorders",
                columns: table => new
                {
                    departureorderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departureType = table.Column<int>(type: "int", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    departureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    motive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    tax = table.Column<float>(type: "real", nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    documentReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departureorders", x => x.departureorderId);
                    table.ForeignKey(
                        name: "FK_Departureorders_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartureOrderDetails",
                columns: table => new
                {
                    DepartureOrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    storeId = table.Column<int>(type: "int", nullable: false),
                    unitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    departedQuantity = table.Column<int>(type: "int", nullable: false),
                    unitPrice = table.Column<float>(type: "real", nullable: false),
                    lote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartureOrderDetails", x => x.DepartureOrderDetailId);
                    table.ForeignKey(
                        name: "FK_DepartureOrderDetails_Departureorders_DepartureOrderId",
                        column: x => x.DepartureOrderId,
                        principalTable: "Departureorders",
                        principalColumn: "departureorderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartureOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartureOrderDetails_Stores_storeId",
                        column: x => x.storeId,
                        principalTable: "Stores",
                        principalColumn: "storeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartureOrderDetails_DepartureOrderId",
                table: "DepartureOrderDetails",
                column: "DepartureOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartureOrderDetails_ProductId",
                table: "DepartureOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartureOrderDetails_storeId",
                table: "DepartureOrderDetails",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departureorders_clientId",
                table: "Departureorders",
                column: "clientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartureOrderDetails");

            migrationBuilder.DropTable(
                name: "Departureorders");
        }
    }
}
