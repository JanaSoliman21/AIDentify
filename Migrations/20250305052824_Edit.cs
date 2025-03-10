using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_payDate_PayDateId",
                table: "Subscription");

            migrationBuilder.DropTable(
                name: "payDate");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_PayDateId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "PayDateId",
                table: "Subscription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayDateId",
                table: "Subscription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payDate",
                columns: table => new
                {
                    PayDateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payDate", x => x.PayDateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_PayDateId",
                table: "Subscription",
                column: "PayDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_payDate_PayDateId",
                table: "Subscription",
                column: "PayDateId",
                principalTable: "payDate",
                principalColumn: "PayDateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
