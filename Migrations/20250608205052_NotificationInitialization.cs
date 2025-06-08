using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class NotificationInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Doctor_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Student_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_Doctor_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctor",
                        principalColumn: "Doctor_ID");
                    table.ForeignKey(
                        name: "FK_Notification_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_Student_Student_ID",
                        column: x => x.Student_ID,
                        principalTable: "Student",
                        principalColumn: "Student_ID");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
