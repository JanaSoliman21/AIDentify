using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class ChatDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Chat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    BlockedIdD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockedIdS = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockerIdD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockerIdS = table.Column<string>(type: "nvarchar(450)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverIdD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverIdS = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SenderIdD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SenderIdS = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Doctor_ReceiverIdD",
                        column: x => x.ReceiverIdD,
                        principalTable: "Doctor",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Doctor_SenderIdD",
                        column: x => x.SenderIdD,
                        principalTable: "Doctor",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Student_ReceiverIdS",
                        column: x => x.ReceiverIdS,
                        principalTable: "Student",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Student_SenderIdS",
                        column: x => x.SenderIdS,
                        principalTable: "Student",
                        principalColumn: "Student_ID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverIdD",
                table: "Message",
                column: "ReceiverIdD");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverIdS",
                table: "Message",
                column: "ReceiverIdS");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderIdD",
                table: "Message",
                column: "SenderIdD");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderIdS",
                table: "Message",
                column: "SenderIdS");
        }
    }
}
