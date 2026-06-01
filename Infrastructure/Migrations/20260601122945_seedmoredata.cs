using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedmoredata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AudioUrl", "Content", "CreatedAt", "Explanation", "ImageUrl", "IsActive", "IsDeleted", "QuestionTypes", "TopicId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-000000000001"), null, "She ___ to school every day.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động lặp đi lặp lại ở hiện tại.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000002"), null, "They usually [play] basketball on weekends.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Có trạng từ 'usually' chỉ thói quen.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000003"), null, "Look! The cat ___ over the wall.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang xảy ra lúc nói ('Look!').", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000004"), null, "I [am studying] for my TOEIC exam right now.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Có trạng từ 'right now'.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000005"), null, "She ___ three cups of coffee today.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đã hoàn thành tính đến hiện tại.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000006"), null, "We [have seen] this movie before.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trải nghiệm tính đến thời điểm hiện tại ('before').", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000007"), null, "I ___ for two hours. My eyes are tired.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh quá trình kéo dài 2 tiếng và để lại hậu quả hiện tại.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000008"), null, "It [has been raining] since morning.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh quá trình bắt đầu từ sáng và vẫn đang tiếp diễn.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000009"), null, "He ___ to Paris last year.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đã kết thúc trong quá khứ ('last year').", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000010"), null, "They [won] the match yesterday.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự việc kết thúc hôm qua ('yesterday').", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000011"), null, "I ___ TV when the phone rang.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang xảy ra thì có hành động khác xen vào.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000012"), null, "While we [were playing], it started to rain.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang kéo dài trong quá khứ ('While').", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000013"), null, "By the time I arrived, they ___.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động xảy ra trước một thời điểm trong quá khứ.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000014"), null, "She told me she [had finished] the job.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động hoàn thành trước khi hành động 'told' xảy ra.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000015"), null, "They ___ for hours before the rescue team arrived.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh quá trình kéo dài trước một mốc quá khứ.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000016"), null, "I [had been studying] English for a year before I visited London.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động học kéo dài liên tục trước khi đến London.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000017"), null, "I think it ___ tomorrow.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dự đoán không có căn cứ rõ ràng ('I think').", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000018"), null, "Don't worry, she [will call] you back later.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một lời hứa hoặc quyết định ngay lúc nói.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000019"), null, "This time next week, I ___ on a beach.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động sẽ đang diễn ra tại một thời điểm xác định trong tương lai.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000020"), null, "They [will be having] dinner when we arrive tonight.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang diễn ra trong tương lai thì bị xen vào.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000021"), null, "By next year, I ___ my graduation project.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động sẽ hoàn thành trước một mốc thời gian tương lai.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000022"), null, "They [will have built] the new bridge by July.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoàn thành trước tháng 7 tới.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000023"), null, "By next month, he ___ here for 5 years.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh khoảng thời gian kéo dài tính đến tương lai.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000024"), null, "By the time you wake up, I [will have been driving] for 3 hours.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động kéo dài liên tục đến lúc bạn thức dậy.", null, true, false, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "12 thì cơ bản trong tiếng anh.", "12 Thì Trong Tiếng Anh" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Content", "CreatedAt", "IsActive", "IsCorrect", "IsDeleted", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000001"), "go", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000002"), "goes", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000003"), "going", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000004"), "is going", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000005"), "play", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000006"), "jumps", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000007"), "is jumping", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000008"), "am studying", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000009"), "drank", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000010"), "has drunk", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000011"), "is drinking", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000012"), "have seen", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000013"), "am reading", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000014"), "have read", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000015"), "have been reading", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000016"), "read", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000017"), "has been raining", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000018"), "went", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000019"), "goes", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000020"), "won", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000021"), "watched", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000022"), "was watching", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000023"), "am watching", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000024"), "were playing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000025"), "left", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000026"), "had left", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000027"), "leave", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000028"), "were leaving", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000029"), "had finished", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000014"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000030"), "had been walking", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000031"), "walked", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000032"), "had been studying", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000016"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000033"), "will rain", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000034"), "rains", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000035"), "is raining", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000036"), "will call", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000018"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000037"), "will be relaxing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000038"), "will relax", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000039"), "relax", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000040"), "am relaxing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000041"), "will be having", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000020"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000042"), "will finish", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000043"), "will have finished", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000044"), "will have built", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000022"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000045"), "will work", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000046"), "works", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000047"), "will have been working", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000048"), "will have been driving", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000024"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ExamDetails",
                columns: new[] { "ExamId", "QuestionId", "Score" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000001"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000002"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000003"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000004"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000005"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000006"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000007"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000008"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000009"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000010"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000011"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000012"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000013"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000014"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000015"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000016"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000017"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000018"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000019"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000020"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000021"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000022"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000023"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000024"), 1.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000001"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000002"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000003"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000004"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000005"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000006"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000007"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000008"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000009"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000010"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000011"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000012"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000013"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000014"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000015"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000016"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000017"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000018"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000019"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000020"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000021"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000022"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000023"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000024"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000025"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000026"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000027"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000028"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000029"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000030"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000031"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000032"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000033"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000034"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000035"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000036"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000037"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000038"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000039"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000040"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000041"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000042"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000043"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000044"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000045"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000046"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000047"));

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-000000000048"));

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000001") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000002") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000003") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000004") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000005") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000006") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000007") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000008") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000009") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000010") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000011") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000012") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000013") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000014") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000015") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000016") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000017") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000018") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000019") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000020") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000021") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000022") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000023") });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000024") });

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000001"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000002"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000003"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000004"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000005"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000006"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000007"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000008"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000009"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000010"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000011"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000012"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000013"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000014"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000015"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000016"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000017"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000018"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000019"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000020"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000021"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000022"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000023"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-000000000024"));

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AudioUrl", "Content", "CreatedAt", "Explanation", "ImageUrl", "IsActive", "IsDeleted", "QuestionTypes", "TopicId", "UpdatedAt" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), null, "She ___ to school every day.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "With third person singular, use 'goes'.", null, true, false, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Basic grammar rules", "Basic Grammar" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Content", "CreatedAt", "IsActive", "IsCorrect", "IsDeleted", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), "go", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "goes", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "going", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ExamDetails",
                columns: new[] { "ExamId", "QuestionId", "Score" },
                values: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-333333333333"), 1.0 });
        }
    }
}
