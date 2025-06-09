using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class Notification3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Doctor_Doctor_ID",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Student_Student_ID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_Doctor_ID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_DoctorId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_Student_ID",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_StudentId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Doctor_ID",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Student_ID",
                table: "Notification");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_DoctorId",
                table: "Notification",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_StudentId",
                table: "Notification",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notification_DoctorId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_StudentId",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "Doctor_ID",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Student_ID",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Doctor_ID",
                table: "Notification",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_DoctorId",
                table: "Notification",
                column: "DoctorId",
                unique: true,
                filter: "[DoctorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Student_ID",
                table: "Notification",
                column: "Student_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_StudentId",
                table: "Notification",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Doctor_Doctor_ID",
                table: "Notification",
                column: "Doctor_ID",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Student_Student_ID",
                table: "Notification",
                column: "Student_ID",
                principalTable: "Student",
                principalColumn: "Student_ID");
        }
    }
}
