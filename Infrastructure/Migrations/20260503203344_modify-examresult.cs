using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyexamresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailResults_Questions_QuestionId",
                table: "DetailResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults");

            migrationBuilder.DropIndex(
                name: "IX_ExamResults_ExamId",
                table: "ExamResults");

            migrationBuilder.DropIndex(
                name: "IX_DetailResults_QuestionId",
                table: "DetailResults");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExamResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "ExamResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ExamResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "DetailResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "DetailResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DetailResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OptionsJson",
                table: "DetailResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "DetailResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuestionTypes",
                table: "DetailResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "DetailResults");

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "DetailResults");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DetailResults");

            migrationBuilder.DropColumn(
                name: "OptionsJson",
                table: "DetailResults");

            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "DetailResults");

            migrationBuilder.DropColumn(
                name: "QuestionTypes",
                table: "DetailResults");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_ExamId",
                table: "ExamResults",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailResults_QuestionId",
                table: "DetailResults",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailResults_Questions_QuestionId",
                table: "DetailResults",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
