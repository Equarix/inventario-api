using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class boxs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxs",
                columns: table => new
                {
                    boxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOpening = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateClosing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    amountOpening = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    amountClosing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    userOpeningId = table.Column<int>(type: "int", nullable: false),
                    userClosingId = table.Column<int>(type: "int", nullable: true),
                    userActualId = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxs");
        }
    }
}
