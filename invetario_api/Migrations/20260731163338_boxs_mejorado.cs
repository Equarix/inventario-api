using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class boxs_mejorado : Migration
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
                    boxName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serieProforma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    storeId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxs", x => x.boxId);
                    table.ForeignKey(
                        name: "FK_Boxs_Stores_storeId",
                        column: x => x.storeId,
                        principalTable: "Stores",
                        principalColumn: "storeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxs_storeId",
                table: "Boxs",
                column: "storeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxs");
        }
    }
}
