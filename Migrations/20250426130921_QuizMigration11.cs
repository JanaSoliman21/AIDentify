using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIDentify.Migrations
{
    /// <inheritdoc />
    public partial class QuizMigration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Student_ID",
                table: "QuizAttempt",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempt_Student_ID",
                table: "QuizAttempt",
                column: "Student_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempt_Student_Student_ID",
                table: "QuizAttempt",
                column: "Student_ID",
                principalTable: "Student",
                principalColumn: "Student_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempt_Student_Student_ID",
                table: "QuizAttempt");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttempt_Student_ID",
                table: "QuizAttempt");

            migrationBuilder.DropColumn(
                name: "Student_ID",
                table: "QuizAttempt");
        }
    }
}
