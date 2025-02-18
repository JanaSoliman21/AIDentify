﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class AfterEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SystemUpdate");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "PayDate");

            migrationBuilder.DropTable(
                name: "Plan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayDate",
                columns: table => new
                {
                    PayDateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayDate", x => x.PayDateId);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    WayOfPayment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Updateable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Accuracy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralFeedback = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModelItSelf = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PayDateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_PayDate_PayDateId",
                        column: x => x.PayDateId,
                        principalTable: "PayDate",
                        principalColumn: "PayDateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    ResultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeValue = table.Column<int>(type: "int", nullable: true),
                    DiseaseValue = table.Column<byte>(type: "tinyint", nullable: true),
                    GenderValue = table.Column<int>(type: "int", nullable: true),
                    TeethNumberingValue = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReviewItSelf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.id);
                    table.ForeignKey(
                        name: "FK_Review_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResultId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriberId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_User_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUpdate",
                columns: table => new
                {
                    SystemUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false),
                    UpdatedDescribtion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUpdate", x => x.SystemUpdateId);
                    table.ForeignKey(
                        name: "FK_SystemUpdate_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_PlanId",
                table: "Models",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ResultId",
                table: "Report",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SubscriberId",
                table: "Report",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_ModelId",
                table: "Result",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ModelId",
                table: "Review",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_PayDateId",
                table: "Subscription",
                column: "PayDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_PlanId",
                table: "Subscription",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUpdate_AdminId",
                table: "SystemUpdate",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PaymentId",
                table: "User",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SubscriptionId",
                table: "User",
                column: "SubscriptionId");
        }
    }
}
