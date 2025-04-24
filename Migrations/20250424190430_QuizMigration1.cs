using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class QuizMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuizId1",
                table: "QuizAttempt",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_QuizId1",
                table: "QuizAttempt",
                column: "QuizId1");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempt_Quiz_QuizId1",
                table: "QuizAttempt",
                column: "QuizId1",
                principalTable: "Quiz",
                principalColumn: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempt_Quiz_QuizId1",
                table: "QuizAttempt");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttempt_QuizId1",
                table: "QuizAttempt");

            migrationBuilder.DropColumn(
                name: "QuizId1",
                table: "QuizAttempt");
        }
    }
}
