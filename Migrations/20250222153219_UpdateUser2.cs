using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_ReceiverId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Message",
                newName: "SenderIdS");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Message",
                newName: "SenderIdD");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                newName: "IX_Message_SenderIdS");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ReceiverId",
                table: "Message",
                newName: "IX_Message_SenderIdD");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverIdD",
                table: "Message",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverIdS",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverIdS",
                table: "Message",
                column: "ReceiverIdS");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropIndex(
                name: "IX_Message_ReceiverIdS",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ReceiverIdD",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ReceiverIdS",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "SenderIdS",
                table: "Message",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "SenderIdD",
                table: "Message",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderIdS",
                table: "Message",
                newName: "IX_Message_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderIdD",
                table: "Message",
                newName: "IX_Message_ReceiverId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PaymentId",
                table: "User",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SubscriptionId",
                table: "User",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_ReceiverId",
                table: "Message",
                column: "ReceiverId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
