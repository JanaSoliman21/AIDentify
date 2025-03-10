using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Plan_PlanId",
                table: "Subscription");

            migrationBuilder.AddColumn<DateTime>(
                name: "WarningDate",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Plan_PlanId",
                table: "Subscription",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Plan_PlanId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "WarningDate",
                table: "Subscription");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Plan_PlanId",
                table: "Subscription",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
