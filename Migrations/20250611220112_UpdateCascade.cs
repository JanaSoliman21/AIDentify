using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Doctor_DoctorId",
                table: "XRayScan");

            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Student_StudentId",
                table: "XRayScan");

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_Doctor_DoctorId",
                table: "XRayScan",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_Student_StudentId",
                table: "XRayScan",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Doctor_DoctorId",
                table: "XRayScan");

            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Student_StudentId",
                table: "XRayScan");

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_Doctor_DoctorId",
                table: "XRayScan",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_Student_StudentId",
                table: "XRayScan",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
