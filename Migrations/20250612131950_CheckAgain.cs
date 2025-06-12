using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class CheckAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemUpdate_Admin_AdminId",
                table: "SystemUpdate");

            migrationBuilder.AddColumn<string>(
                name: "Admin_ID",
                table: "SystemUpdate",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUpdate_Admin_ID",
                table: "SystemUpdate",
                column: "Admin_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUpdate_Admin_AdminId",
                table: "SystemUpdate",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Admin_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUpdate_Admin_Admin_ID",
                table: "SystemUpdate",
                column: "Admin_ID",
                principalTable: "Admin",
                principalColumn: "Admin_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemUpdate_Admin_AdminId",
                table: "SystemUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUpdate_Admin_Admin_ID",
                table: "SystemUpdate");

            migrationBuilder.DropIndex(
                name: "IX_SystemUpdate_Admin_ID",
                table: "SystemUpdate");

            migrationBuilder.DropColumn(
                name: "Admin_ID",
                table: "SystemUpdate");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUpdate_Admin_AdminId",
                table: "SystemUpdate",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Admin_ID");
        }
    }
}
