using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_User_DoctorId",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempt_User_StudentId",
                table: "QuizAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUpdate_User_AdminId",
                table: "SystemUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_User_DoctorId",
                table: "XRayScan");

            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_User_StudentId",
                table: "XRayScan");

            migrationBuilder.DropColumn(
                name: "ClinicName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TotalPOintsEarned",
                table: "User");

            migrationBuilder.DropColumn(
                name: "University",
                table: "User");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Admin_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Admin_ID);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Doctor_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Doctor_ID);
                    table.ForeignKey(
                        name: "FK_Doctor_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_Doctor_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "SubscriptionId");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Student_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    TotalPOintsEarned = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Student_ID);
                    table.ForeignKey(
                        name: "FK_Student_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_Student_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "SubscriptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_PaymentId",
                table: "Doctor",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_SubscriptionId",
                table: "Doctor",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_PaymentId",
                table: "Student",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SubscriptionId",
                table: "Student",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Doctor_DoctorId",
                table: "Patient",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempt_Student_StudentId",
                table: "QuizAttempt",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUpdate_Admin_AdminId",
                table: "SystemUpdate",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Admin_ID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Doctor_DoctorId",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempt_Student_StudentId",
                table: "QuizAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUpdate_Admin_AdminId",
                table: "SystemUpdate");

            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Doctor_DoctorId",
                table: "XRayScan");

            migrationBuilder.DropForeignKey(
                name: "FK_XRayScan_Student_StudentId",
                table: "XRayScan");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.AddColumn<string>(
                name: "ClinicName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalPOintsEarned",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_User_DoctorId",
                table: "Patient",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempt_User_StudentId",
                table: "QuizAttempt",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUpdate_User_AdminId",
                table: "SystemUpdate",
                column: "AdminId",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_User_DoctorId",
                table: "XRayScan",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_XRayScan_User_StudentId",
                table: "XRayScan",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
