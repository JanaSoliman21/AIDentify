using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDelete2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId",
                table: "MedicalHistory");

            migrationBuilder.AddColumn<string>(
                name: "XRayScanId1",
                table: "MedicalHistory",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_XRayScanId1",
                table: "MedicalHistory",
                column: "XRayScanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId",
                table: "MedicalHistory",
                column: "XRayScanId",
                principalTable: "XRayScan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId1",
                table: "MedicalHistory",
                column: "XRayScanId1",
                principalTable: "XRayScan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId",
                table: "MedicalHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId1",
                table: "MedicalHistory");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistory_XRayScanId1",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "XRayScanId1",
                table: "MedicalHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId",
                table: "MedicalHistory",
                column: "XRayScanId",
                principalTable: "XRayScan",
                principalColumn: "Id");
        }
    }
}
