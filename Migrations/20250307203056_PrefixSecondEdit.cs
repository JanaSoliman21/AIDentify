using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class PrefixSecondEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Subscription",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Plan",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payment",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subscription",
                newName: "SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Plan",
                newName: "PlanId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Payment",
                newName: "PaymentId");
        }
    }
}
