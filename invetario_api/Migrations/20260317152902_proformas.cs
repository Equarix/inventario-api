using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class proformas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proformas",
                columns: table => new
                {
                    proformaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    storeId = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proformas", x => x.proformaId);
                    table.ForeignKey(
                        name: "FK_Proformas_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proformas_Stores_storeId",
                        column: x => x.storeId,
                        principalTable: "Stores",
                        principalColumn: "storeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proformas_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProformaDetails",
                columns: table => new
                {
                    proformaDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proformaId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaDetails", x => x.proformaDetailsId);
                    table.ForeignKey(
                        name: "FK_ProformaDetails_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProformaDetails_Proformas_proformaId",
                        column: x => x.proformaId,
                        principalTable: "Proformas",
                        principalColumn: "proformaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProformaDetails_productId",
                table: "ProformaDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaDetails_proformaId",
                table: "ProformaDetails",
                column: "proformaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proformas_clientId",
                table: "Proformas",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Proformas_storeId",
                table: "Proformas",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_Proformas_userId",
                table: "Proformas",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProformaDetails");

            migrationBuilder.DropTable(
                name: "Proformas");
        }
    }
}
