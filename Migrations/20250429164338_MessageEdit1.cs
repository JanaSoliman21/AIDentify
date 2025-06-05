using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class MessageEdit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Context",
                table: "Message",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Message",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlockerIdD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockedIdD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockerIdS = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockedIdS = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Block_Doctor_BlockedIdD",
                        column: x => x.BlockedIdD,
                        principalTable: "Doctor",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Block_Doctor_BlockerIdD",
                        column: x => x.BlockerIdD,
                        principalTable: "Doctor",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Block_Student_BlockedIdS",
                        column: x => x.BlockedIdS,
                        principalTable: "Student",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Block_Student_BlockerIdS",
                        column: x => x.BlockerIdS,
                        principalTable: "Student",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockedIdD",
                table: "Block",
                column: "BlockedIdD");

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockedIdS",
                table: "Block",
                column: "BlockedIdS");

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockerIdD",
                table: "Block",
                column: "BlockerIdD");

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockerIdS",
                table: "Block",
                column: "BlockerIdS");

            migrationBuilder.CreateIndex(
                name: "IX_Block_ChatId",
                table: "Block",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Message",
                newName: "Context");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Message",
                newName: "MessageId");
        }
    }
}
