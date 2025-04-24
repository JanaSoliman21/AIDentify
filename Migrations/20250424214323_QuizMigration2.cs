using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class QuizMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuizAttemptId",
                table: "QuizAttempt",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Quiz",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Question",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "QuizAttempt",
                newName: "QuizAttemptId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Quiz",
                newName: "QuizId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Question",
                newName: "QuestionId");
        }
    }
}
