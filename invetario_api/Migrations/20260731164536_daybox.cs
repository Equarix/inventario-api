using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class daybox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dayboxs",
                columns: table => new
                {
                    dayboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    boxId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    totalefectivo = table.Column<float>(type: "real", nullable: false),
                    totalTarjeta = table.Column<float>(type: "real", nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dayboxs", x => x.dayboxId);
                    table.ForeignKey(
                        name: "FK_Dayboxs_Boxs_boxId",
                        column: x => x.boxId,
                        principalTable: "Boxs",
                        principalColumn: "boxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dayboxs_boxId",
                table: "Dayboxs",
                column: "boxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dayboxs");
        }
    }
}
