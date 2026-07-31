using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invetario_api.Migrations
{
    /// <inheritdoc />
    public partial class user_box : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Dayboxs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dayboxs_userId",
                table: "Dayboxs",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dayboxs_Users_userId",
                table: "Dayboxs",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dayboxs_Users_userId",
                table: "Dayboxs");

            migrationBuilder.DropIndex(
                name: "IX_Dayboxs_userId",
                table: "Dayboxs");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Dayboxs");
        }
    }
}
