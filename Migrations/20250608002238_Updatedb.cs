using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class Updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Patient_PatientId",
                table: "XRayScan");

            migrationBuilder.DropIndex(
                name: "IX_XRayScan_PatientId",
                table: "XRayScan");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "XRayScan");

            migrationBuilder.DropColumn(
                name: "TeethCount",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "XRay",
                table: "MedicalHistory");

            migrationBuilder.RenameColumn(
                name: "ScanId",
                table: "XRayScan",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patient",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MedicalHistoryId",
                table: "MedicalHistory",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeethPrediction",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DiseasePrediction",
                table: "MedicalHistory",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeethPrediction",
                table: "MedicalHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XRayScanId",
                table: "MedicalHistory",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_XRayScanId",
                table: "MedicalHistory",
                column: "XRayScanId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId",
                table: "MedicalHistory",
                column: "XRayScanId",
                principalTable: "XRayScan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_XRayScan_XRayScanId",
                table: "MedicalHistory");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistory_XRayScanId",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "TeethPrediction",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DiseasePrediction",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "TeethPrediction",
                table: "MedicalHistory");

            migrationBuilder.DropColumn(
                name: "XRayScanId",
                table: "MedicalHistory");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "XRayScan",
                newName: "ScanId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patient",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MedicalHistory",
                newName: "MedicalHistoryId");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "XRayScan",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeethCount",
                table: "MedicalHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "XRay",
                table: "MedicalHistory",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_XRayScan_PatientId",
                table: "XRayScan",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_Patient_PatientId",
                table: "XRayScan",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
