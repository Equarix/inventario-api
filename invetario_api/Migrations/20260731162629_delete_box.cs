using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class delete_box : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxmoves");

            migrationBuilder.DropTable(
                name: "Boxs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxs",
                columns: table => new
                {
                    boxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userActualId = table.Column<int>(type: "int", nullable: false),
                    userClosingId = table.Column<int>(type: "int", nullable: true),
                    userOpeningId = table.Column<int>(type: "int", nullable: false),
                    amountClosing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    amountOpening = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dateClosing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateOpening = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxs", x => x.boxId);
                    table.ForeignKey(
                        name: "FK_Boxs_Users_userActualId",
                        column: x => x.userActualId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boxs_Users_userClosingId",
                        column: x => x.userClosingId,
                        principalTable: "Users",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_Boxs_Users_userOpeningId",
                        column: x => x.userOpeningId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boxmoves",
                columns: table => new
                {
                    boxMoveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    boxId = table.Column<int>(type: "int", nullable: false),
                    paymentMethodId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    dateMove = table.Column<DateTime>(type: "datetime2", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxmoves", x => x.boxMoveId);
                    table.ForeignKey(
                        name: "FK_Boxmoves_Boxs_boxId",
                        column: x => x.boxId,
                        principalTable: "Boxs",
                        principalColumn: "boxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boxmoves_Paymethods_paymentMethodId",
                        column: x => x.paymentMethodId,
                        principalTable: "Paymethods",
                        principalColumn: "paymethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boxmoves_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxmoves_boxId",
                table: "Boxmoves",
                column: "boxId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxmoves_paymentMethodId",
                table: "Boxmoves",
                column: "paymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxmoves_userId",
                table: "Boxmoves",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_userActualId",
                table: "Boxs",
                column: "userActualId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_userClosingId",
                table: "Boxs",
                column: "userClosingId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_userOpeningId",
                table: "Boxs",
                column: "userOpeningId");
        }
    }
}
