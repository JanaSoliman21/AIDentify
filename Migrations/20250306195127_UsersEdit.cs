using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class UsersEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Payment_PaymentId",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Doctor_ReceiverIdS",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Doctor_SenderIdS",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Student_ReceiverIdS",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Student_SenderIdD",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Payment_PaymentId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_PaymentId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_PaymentId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Subscription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Subscription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverIdD",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_DoctorId",
                table: "Subscription",
                column: "DoctorId",
                unique: true,
                filter: "[DoctorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_StudentId",
                table: "Subscription",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_DoctorId",
                table: "Payment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_StudentId",
                table: "Payment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverIdD",
                table: "Message",
                column: "ReceiverIdD");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Doctor_ReceiverIdD",
                table: "Message",
                column: "ReceiverIdD",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Doctor_SenderIdD",
                table: "Message",
                column: "SenderIdD",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Student_ReceiverIdS",
                table: "Message",
                column: "ReceiverIdS",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Student_SenderIdS",
                table: "Message",
                column: "SenderIdS",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Doctor_DoctorId",
                table: "Payment",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Student_StudentId",
                table: "Payment",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Doctor_DoctorId",
                table: "Subscription",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Student_StudentId",
                table: "Subscription",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Student_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Doctor_ReceiverIdD",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Doctor_SenderIdD",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Student_ReceiverIdS",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Student_SenderIdS",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Doctor_DoctorId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Student_StudentId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Doctor_DoctorId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Student_StudentId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_DoctorId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_StudentId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Payment_DoctorId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_StudentId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Message_ReceiverIdD",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverIdD",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Doctor",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_PaymentId",
                table: "Student",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_PaymentId",
                table: "Doctor",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Payment_PaymentId",
                table: "Doctor",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Doctor_ReceiverIdS",
                table: "Message",
                column: "ReceiverIdS",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Doctor_SenderIdS",
                table: "Message",
                column: "SenderIdS",
                principalTable: "Doctor",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Student_ReceiverIdS",
                table: "Message",
                column: "ReceiverIdS",
                principalTable: "Student",
                principalColumn: "Student_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Student_SenderIdD",
                table: "Message",
                column: "SenderIdD",
                principalTable: "Student",
                principalColumn: "Student_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Payment_PaymentId",
                table: "Student",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId");
        }
    }
}
