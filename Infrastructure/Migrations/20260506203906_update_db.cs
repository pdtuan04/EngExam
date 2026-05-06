using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Topics_TopicId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailResults_ExamResults_ExamResultId",
                table: "DetailResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamDetails_Exams_ExamId",
                table: "ExamDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamDetails_Questions_QuestionId",
                table: "ExamDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_AspNetUsers_UserId",
                table: "ExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamCategories_ExamCategoryId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_PracticeDetails_Practices_PracticeId",
                table: "PracticeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PracticeDetails_Questions_QuestionId",
                table: "PracticeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Topics_TopicId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_FlashCards_FlashCardId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_FlashCardId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TopicId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Practices_TopicId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_PracticeDetails_QuestionId",
                table: "PracticeDetails");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ExamCategoryId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_ExamResults_UserId",
                table: "ExamResults");

            migrationBuilder.DropIndex(
                name: "IX_ExamDetails_QuestionId",
                table: "ExamDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailResults",
                table: "DetailResults");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TopicId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "DetailResults");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Words",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Topics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Practices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FlashCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FlashCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FlashCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailResults",
                table: "DetailResults",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("2af67565-75f7-4511-9b67-3762e917c173"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "UpdatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailResults",
                table: "DetailResults");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FlashCards");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Topics",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Questions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Practices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Exams",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamCategories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "DetailResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Answers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailResults",
                table: "DetailResults",
                columns: new[] { "ExamResultId", "QuestionId" });

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("2af67565-75f7-4511-9b67-3762e917c173"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExamCategories",
                keyColumn: "Id",
                keyValue: new Guid("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Words_FlashCardId",
                table: "Words",
                column: "FlashCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TopicId",
                table: "Questions",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_TopicId",
                table: "Practices",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeDetails_QuestionId",
                table: "PracticeDetails",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamCategoryId",
                table: "Exams",
                column: "ExamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_UserId",
                table: "ExamResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetails_QuestionId",
                table: "ExamDetails",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TopicId",
                table: "Courses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Topics_TopicId",
                table: "Courses",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailResults_ExamResults_ExamResultId",
                table: "DetailResults",
                column: "ExamResultId",
                principalTable: "ExamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamDetails_Exams_ExamId",
                table: "ExamDetails",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamDetails_Questions_QuestionId",
                table: "ExamDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_AspNetUsers_UserId",
                table: "ExamResults",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamCategories_ExamCategoryId",
                table: "Exams",
                column: "ExamCategoryId",
                principalTable: "ExamCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeDetails_Practices_PracticeId",
                table: "PracticeDetails",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticeDetails_Questions_QuestionId",
                table: "PracticeDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Topics_TopicId",
                table: "Practices",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_FlashCards_FlashCardId",
                table: "Words",
                column: "FlashCardId",
                principalTable: "FlashCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
