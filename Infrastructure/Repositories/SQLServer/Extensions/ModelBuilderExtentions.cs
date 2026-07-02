using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories.SQLServer.Extensions
{
    public static class ModelBuilderExtentions
    {
        public static void SeedingData(this ModelBuilder modelBuilder)
        {
            // ================= 1. IDENTITY (ROLES & USERS) =================
            var userRole = new IdentityRole<Guid>
            {
                Id = Guid.Parse("05f2400b-5471-466a-8b7e-27752367e4d6"),
                Name = "User",
                NormalizedName = "USER"
            };

            var adminRole = new IdentityRole<Guid>
            {
                Id = Guid.Parse("10f2400b-5471-466a-8b7e-27752367e4d6"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(userRole, adminRole);

            var admin = new User()
            {
                Id = Guid.Parse("9ae1058d-b602-4025-ab1d-74e7bced8f3b"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEFY87mzNg88TIJtuXRcRIeT0MXYto4NkcukxwFGpl+p5IHBJVqlPbyFx9UJIOmu7eA==",
                SecurityStamp = "3XVVZIW5RPRWT7MKN3Y6VRNTHXY2JGK5",
                ConcurrencyStamp = "6e66d8c1-89da-46df-bc24-ec54c7e7e7cf"
            };

            var user = new User()
            {
                Id = Guid.Parse("8d581a98-361e-4333-a651-74e88ef572a4"),
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEN8TWXW9pNZ+VVyeftOLixsSfyDOtPTZpv84QtbFESyzd6kZ0i70eIPvnvNBKX0Q9Q==",
                SecurityStamp = "DF7GIIY7UNBVCVLZD73QO6PGSVQXBSTW",
                ConcurrencyStamp = "f67e2437-61a2-4458-ac14-de7ab48158b6"
            };

            modelBuilder.Entity<User>().HasData(new List<User>() { admin, user });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid> { RoleId = adminRole.Id, UserId = admin.Id },
                new IdentityUserRole<Guid> { RoleId = userRole.Id, UserId = user.Id }
            });

            // ================= 2. CATEGORIES & TOPICS =================
            var seedDate = new DateTime(2026, 01, 01);
            var topicId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var examId = Guid.Parse("77777777-7777-7777-7777-777777777777");

            modelBuilder.Entity<ExamCategory>().HasData(
                new ExamCategory { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Grammar", Description = "Grammar examination category", ImageUrl = "images/category_img.jpg", CreatedAt = seedDate, IsActive = true },
                new ExamCategory { Id = Guid.Parse("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"), Name = "Reading", Description = "Reading", ImageUrl = "images/category_img.jpg", CreatedAt = seedDate, IsActive = true },
                new ExamCategory { Id = Guid.Parse("2af67565-75f7-4511-9b67-3762e917c173"), Name = "Vocabulary", Description = "Vocabulary exam", ImageUrl = "images/category_img.jpg", CreatedAt = seedDate, IsActive = true },
                new ExamCategory { Id = Guid.Parse("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"), Name = "Listening", Description = "Listening exam", ImageUrl = "images/category_img.jpg", CreatedAt = seedDate, IsActive = true }
            );

            modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = topicId, Name = "12 Thì Trong Tiếng Anh", Description = "12 thì cơ bản trong tiếng anh.", CreatedAt = seedDate, IsActive = true }
            );

            // ================= 3. QUESTIONS (MIXED MC & FIB ĐÃ FIX FORMAT) =================
            var mc = Domain.Enums.QuestionTypes.MultipleChoice;
            var fib = Domain.Enums.QuestionTypes.FillInTheBlank;

            modelBuilder.Entity<Question>().HasData(
                // 1. Present Simple
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000001"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "She ___ to school every day.", Explanation = "Hành động lặp đi lặp lại ở hiện tại." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000002"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "They usually ___ (play) basketball on weekends.", Explanation = "Có trạng từ 'usually' chỉ thói quen." },

                // 2. Present Continuous
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000003"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "Look! The cat ___ over the wall.", Explanation = "Hành động đang xảy ra lúc nói ('Look!')." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000004"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "I ___ (study) for my TOEIC exam right now.", Explanation = "Có trạng từ 'right now'." },

                // 3. Present Perfect
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000005"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "She ___ three cups of coffee today.", Explanation = "Hành động đã hoàn thành tính đến hiện tại." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000006"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "We ___ (see) this movie before.", Explanation = "Trải nghiệm tính đến thời điểm hiện tại ('before')." },

                // 4. Present Perfect Continuous
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000007"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "I ___ for two hours. My eyes are tired.", Explanation = "Nhấn mạnh quá trình kéo dài 2 tiếng và để lại hậu quả hiện tại." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000008"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "It ___ (rain) since morning.", Explanation = "Nhấn mạnh quá trình bắt đầu từ sáng và vẫn đang tiếp diễn." },

                // 5. Past Simple
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000009"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "He ___ to Paris last year.", Explanation = "Hành động đã kết thúc trong quá khứ ('last year')." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000010"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "They ___ (win) the match yesterday.", Explanation = "Sự việc kết thúc hôm qua ('yesterday')." },

                // 6. Past Continuous
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000011"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "I ___ TV when the phone rang.", Explanation = "Hành động đang xảy ra thì có hành động khác xen vào." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000012"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "While we ___ (play), it started to rain.", Explanation = "Hành động đang kéo dài trong quá khứ ('While')." },

                // 7. Past Perfect
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000013"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "By the time I arrived, they ___.", Explanation = "Hành động xảy ra trước một thời điểm trong quá khứ." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000014"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "She told me she ___ (finish) the job.", Explanation = "Hành động hoàn thành trước khi hành động 'told' xảy ra." },

                // 8. Past Perfect Continuous
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000015"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "They ___ for hours before the rescue team arrived.", Explanation = "Nhấn mạnh quá trình kéo dài trước một mốc quá khứ." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000016"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "I ___ (study) English for a year before I visited London.", Explanation = "Hành động học kéo dài liên tục trước khi đến London." },

                // 9. Future Simple
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000017"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "I think it ___ tomorrow.", Explanation = "Dự đoán không có căn cứ rõ ràng ('I think')." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000018"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "Don't worry, she ___ (call) you back later.", Explanation = "Một lời hứa hoặc quyết định ngay lúc nói." },

                // 10. Future Continuous
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000019"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "This time next week, I ___ on a beach.", Explanation = "Hành động sẽ đang diễn ra tại một thời điểm xác định trong tương lai." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000020"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "They ___ (have) dinner when we arrive tonight.", Explanation = "Hành động đang diễn ra trong tương lai thì bị xen vào." },

                // 11. Future Perfect
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000021"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "By next year, I ___ my graduation project.", Explanation = "Hành động sẽ hoàn thành trước một mốc thời gian tương lai." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000022"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "They ___ (build) the new bridge by July.", Explanation = "Hoàn thành trước tháng 7 tới." },

                // 12. Future Perfect Continuous
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000023"), TopicId = topicId, QuestionTypes = mc, CreatedAt = seedDate, IsActive = true, Content = "By next month, he ___ here for 5 years.", Explanation = "Nhấn mạnh khoảng thời gian kéo dài tính đến tương lai." },
                new Question { Id = Guid.Parse("33333333-3333-3333-3333-000000000024"), TopicId = topicId, QuestionTypes = fib, CreatedAt = seedDate, IsActive = true, Content = "By the time you wake up, I ___ (drive) for 3 hours.", Explanation = "Hành động kéo dài liên tục đến lúc bạn thức dậy." }
            );

            // ================= 4. ANSWERS (48 ĐÁP ÁN) =================
            modelBuilder.Entity<Answer>().HasData(
                // Q1 (MC - 4 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000001"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000001"), CreatedAt = seedDate, IsActive = true, Content = "go", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000002"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000001"), CreatedAt = seedDate, IsActive = true, Content = "goes", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000003"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000001"), CreatedAt = seedDate, IsActive = true, Content = "going", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000004"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000001"), CreatedAt = seedDate, IsActive = true, Content = "is going", IsCorrect = false },

                // Q2 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000005"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000002"), CreatedAt = seedDate, IsActive = true, Content = "play", IsCorrect = true },

                // Q3 (MC - 2 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000006"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000003"), CreatedAt = seedDate, IsActive = true, Content = "jumps", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000007"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000003"), CreatedAt = seedDate, IsActive = true, Content = "is jumping", IsCorrect = true },

                // Q4 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000008"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000004"), CreatedAt = seedDate, IsActive = true, Content = "am studying", IsCorrect = true },

                // Q5 (MC - 3 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000009"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000005"), CreatedAt = seedDate, IsActive = true, Content = "drank", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000010"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000005"), CreatedAt = seedDate, IsActive = true, Content = "has drunk", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000011"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000005"), CreatedAt = seedDate, IsActive = true, Content = "is drinking", IsCorrect = false },

                // Q6 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000012"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000006"), CreatedAt = seedDate, IsActive = true, Content = "have seen", IsCorrect = true },

                // Q7 (MC - 4 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000013"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000007"), CreatedAt = seedDate, IsActive = true, Content = "am reading", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000014"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000007"), CreatedAt = seedDate, IsActive = true, Content = "have read", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000015"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000007"), CreatedAt = seedDate, IsActive = true, Content = "have been reading", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000016"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000007"), CreatedAt = seedDate, IsActive = true, Content = "read", IsCorrect = false },

                // Q8 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000017"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000008"), CreatedAt = seedDate, IsActive = true, Content = "has been raining", IsCorrect = true },

                // Q9 (MC - 2 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000018"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000009"), CreatedAt = seedDate, IsActive = true, Content = "went", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000019"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000009"), CreatedAt = seedDate, IsActive = true, Content = "goes", IsCorrect = false },

                // Q10 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000020"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000010"), CreatedAt = seedDate, IsActive = true, Content = "won", IsCorrect = true },

                // Q11 (MC - 3 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000021"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000011"), CreatedAt = seedDate, IsActive = true, Content = "watched", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000022"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000011"), CreatedAt = seedDate, IsActive = true, Content = "was watching", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000023"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000011"), CreatedAt = seedDate, IsActive = true, Content = "am watching", IsCorrect = false },

                // Q12 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000024"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000012"), CreatedAt = seedDate, IsActive = true, Content = "were playing", IsCorrect = true },

                // Q13 (MC - 4 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000025"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000013"), CreatedAt = seedDate, IsActive = true, Content = "left", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000026"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000013"), CreatedAt = seedDate, IsActive = true, Content = "had left", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000027"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000013"), CreatedAt = seedDate, IsActive = true, Content = "leave", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000028"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000013"), CreatedAt = seedDate, IsActive = true, Content = "were leaving", IsCorrect = false },

                // Q14 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000029"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000014"), CreatedAt = seedDate, IsActive = true, Content = "had finished", IsCorrect = true },

                // Q15 (MC - 2 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000030"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000015"), CreatedAt = seedDate, IsActive = true, Content = "had been walking", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000031"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000015"), CreatedAt = seedDate, IsActive = true, Content = "walked", IsCorrect = false },

                // Q16 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000032"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000016"), CreatedAt = seedDate, IsActive = true, Content = "had been studying", IsCorrect = true },

                // Q17 (MC - 3 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000033"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000017"), CreatedAt = seedDate, IsActive = true, Content = "will rain", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000034"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000017"), CreatedAt = seedDate, IsActive = true, Content = "rains", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000035"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000017"), CreatedAt = seedDate, IsActive = true, Content = "is raining", IsCorrect = false },

                // Q18 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000036"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000018"), CreatedAt = seedDate, IsActive = true, Content = "will call", IsCorrect = true },

                // Q19 (MC - 4 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000037"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000019"), CreatedAt = seedDate, IsActive = true, Content = "will be relaxing", IsCorrect = true },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000038"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000019"), CreatedAt = seedDate, IsActive = true, Content = "will relax", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000039"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000019"), CreatedAt = seedDate, IsActive = true, Content = "relax", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000040"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000019"), CreatedAt = seedDate, IsActive = true, Content = "am relaxing", IsCorrect = false },

                // Q20 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000041"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000020"), CreatedAt = seedDate, IsActive = true, Content = "will be having", IsCorrect = true },

                // Q21 (MC - 2 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000042"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000021"), CreatedAt = seedDate, IsActive = true, Content = "will finish", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000043"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000021"), CreatedAt = seedDate, IsActive = true, Content = "will have finished", IsCorrect = true },

                // Q22 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000044"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000022"), CreatedAt = seedDate, IsActive = true, Content = "will have built", IsCorrect = true },

                // Q23 (MC - 3 options)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000045"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000023"), CreatedAt = seedDate, IsActive = true, Content = "will work", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000046"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000023"), CreatedAt = seedDate, IsActive = true, Content = "works", IsCorrect = false },
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000047"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000023"), CreatedAt = seedDate, IsActive = true, Content = "will have been working", IsCorrect = true },

                // Q24 (FIB - 1 correct option)
                new Answer { Id = Guid.Parse("44444444-4444-4444-4444-000000000048"), QuestionId = Guid.Parse("33333333-3333-3333-3333-000000000024"), CreatedAt = seedDate, IsActive = true, Content = "will have been driving", IsCorrect = true }
            );

            // ================= 5. EXAM & EXAM DETAILS =================
            modelBuilder.Entity<Exam>().HasData(
                new Exam
                {
                    Id = examId,
                    Title = "Basic Grammar Test",
                    Description = "Basic Grammar Test",
                    ExamCategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DurationInMinutes = 10,
                    CreatedAt = seedDate,
                    IsActive = true
                }
            );

            var examDetails = new List<ExamDetail>();
            for (int i = 1; i <= 24; i++)
            {
                string questionIdSuffix = i.ToString("D12");
                examDetails.Add(new ExamDetail
                {
                    ExamId = examId,
                    QuestionId = Guid.Parse($"33333333-3333-3333-3333-{questionIdSuffix}"),
                    Score = 1
                });
            }
            modelBuilder.Entity<ExamDetail>().HasData(examDetails);
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = Guid.Parse("2b82c46a-209b-4c86-b917-9ee78a51efeb"),
                    Name = "12 Thì Trong Tiếng Anh",
                    Description = "Cách dùng và công thức của 12 Thì Trong Tiếng Anh",
                    ImageUrl = "images/fd75ef51-c277-4856-8f5f-a70515953e2d_Screenshot 2026-06-01 210443.png",
                    TopicId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CreatedAt = new DateTime(2026, 06, 01, 14, 17, 35),
                    IsActive = true,
                    IsDeleted = false,
                    Content = """
                    <h1>12 Th&igrave; Trong Tiếng Anh</h1>
                    <h2>Giới thiệu</h2>
                    <p>Th&igrave; (Tense) l&agrave; một trong những phần ngữ ph&aacute;p quan trọng nhất trong tiếng Anh. Việc sử dụng đ&uacute;ng th&igrave; gi&uacute;p người học diễn đạt ch&iacute;nh x&aacute;c thời gian, trạng th&aacute;i v&agrave; qu&aacute; tr&igrave;nh của h&agrave;nh động. Hệ thống ngữ ph&aacute;p tiếng Anh bao gồm 12 th&igrave; cơ bản, được chia th&agrave;nh ba mốc thời gian ch&iacute;nh: hiện tại, qu&aacute; khứ v&agrave; tương lai. Mỗi mốc thời gian lại c&oacute; bốn dạng: đơn, tiếp diễn, ho&agrave;n th&agrave;nh v&agrave; ho&agrave;n th&agrave;nh tiếp diễn.</p>
                    <hr>
                    <h1>I. C&aacute;c th&igrave; hiện tại</h1>
                    <h2>1. Hiện tại đơn (Simple Present)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>Khẳng định: S + V(s/es)</p></li>
                    <li><p>Phủ định: S + do/does not + V</p></li>
                    <li><p>Nghi vấn: Do/Does + S + V?</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả th&oacute;i quen, sở th&iacute;ch.</p></li>
                    <li><p>Diễn tả sự thật hiển nhi&ecirc;n.</p></li>
                    <li><p>Diễn tả lịch tr&igrave;nh, thời gian biểu.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>I go to school every day.</p></li>
                    <li><p>The sun rises in the east.</p></li>
                    </ul>
                    <hr>
                    <h2>2. Hiện tại tiếp diễn (Present Continuous)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + am/is/are + V-ing</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động đang diễn ra tại thời điểm n&oacute;i.</p></li>
                    <li><p>Diễn tả kế hoạch trong tương lai gần.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>She is studying English now.</p></li>
                    <li><p>We are meeting our teacher tomorrow.</p></li>
                    </ul>
                    <hr>
                    <h2>3. Hiện tại ho&agrave;n th&agrave;nh (Present Perfect)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + have/has + V3/ed</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động xảy ra trong qu&aacute; khứ nhưng c&ograve;n li&ecirc;n quan đến hiện tại.</p></li>
                    <li><p>Diễn tả kinh nghiệm hoặc trải nghiệm.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>I have finished my homework.</p></li>
                    <li><p>She has visited Japan twice.</p></li>
                    </ul>
                    <hr>
                    <h2>4. Hiện tại ho&agrave;n th&agrave;nh tiếp diễn (Present Perfect Continuous)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + have/has been + V-ing</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Nhấn mạnh qu&aacute; tr&igrave;nh của h&agrave;nh động bắt đầu trong qu&aacute; khứ v&agrave; vẫn tiếp tục đến hiện tại.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>They have been learning English for three years.</p></li>
                    <li><p>I have been waiting for an hour.</p></li>
                    </ul>
                    <hr>
                    <h1>II. C&aacute;c th&igrave; qu&aacute; khứ</h1>
                    <h2>5. Qu&aacute; khứ đơn (Simple Past)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + V2/ed</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động đ&atilde; xảy ra v&agrave; kết th&uacute;c trong qu&aacute; khứ.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>I visited my grandparents last weekend.</p></li>
                    <li><p>She bought a new laptop yesterday.</p></li>
                    </ul>
                    <hr>
                    <h2>6. Qu&aacute; khứ tiếp diễn (Past Continuous)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + was/were + V-ing</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động đang diễn ra tại một thời điểm trong qu&aacute; khứ.</p></li>
                    <li><p>Diễn tả h&agrave;nh động bị h&agrave;nh động kh&aacute;c xen v&agrave;o.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>I was reading when he called.</p></li>
                    <li><p>They were playing football at 5 p.m.</p></li>
                    </ul>
                    <hr>
                    <h2>7. Qu&aacute; khứ ho&agrave;n th&agrave;nh (Past Perfect)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + had + V3/ed</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động xảy ra trước một h&agrave;nh động kh&aacute;c trong qu&aacute; khứ.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>She had left before I arrived.</p></li>
                    <li><p>They had finished dinner when we came.</p></li>
                    </ul>
                    <hr>
                    <h2>8. Qu&aacute; khứ ho&agrave;n th&agrave;nh tiếp diễn (Past Perfect Continuous)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + had been + V-ing</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Nhấn mạnh qu&aacute; tr&igrave;nh của h&agrave;nh động k&eacute;o d&agrave;i trước một thời điểm hoặc h&agrave;nh động trong qu&aacute; khứ.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>He had been working for five hours before taking a break.</p></li>
                    <li><p>They had been waiting for a long time before the bus arrived.</p></li>
                    </ul>
                    <hr>
                    <h1>III. C&aacute;c th&igrave; tương lai</h1>
                    <h2>9. Tương lai đơn (Simple Future)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + will + V</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả quyết định tức thời.</p></li>
                    <li><p>Dự đo&aacute;n hoặc lời hứa.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>I will help you.</p></li>
                    <li><p>It will rain tomorrow.</p></li>
                    </ul>
                    <hr>
                    <h2>10. Tương lai tiếp diễn (Future Continuous)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + will be + V-ing</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động sẽ đang diễn ra tại một thời điểm trong tương lai.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>This time tomorrow, I will be studying.</p></li>
                    <li><p>They will be traveling next week.</p></li>
                    </ul>
                    <hr>
                    <h2>11. Tương lai ho&agrave;n th&agrave;nh (Future Perfect)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + will have + V3/ed</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Diễn tả h&agrave;nh động sẽ ho&agrave;n th&agrave;nh trước một thời điểm trong tương lai.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>I will have graduated by next year.</p></li>
                    <li><p>She will have completed the project before Friday.</p></li>
                    </ul>
                    <hr>
                    <h2>12. Tương lai ho&agrave;n th&agrave;nh tiếp diễn (Future Perfect Continuous)</h2>
                    <h3>C&ocirc;ng thức</h3>
                    <ul>
                    <li><p>S + will have been + V-ing</p></li>
                    </ul>
                    <h3>C&aacute;ch d&ugrave;ng</h3>
                    <ul>
                    <li><p>Nhấn mạnh khoảng thời gian một h&agrave;nh động k&eacute;o d&agrave;i đến một thời điểm trong tương lai.</p></li>
                    </ul>
                    <h3>V&iacute; dụ</h3>
                    <ul>
                    <li><p>By next month, I will have been working here for two years.</p></li>
                    <li><p>They will have been studying for six hours by midnight.</p></li>
                    </ul>
                    <hr>
                    <h1>Kết luận</h1>
                    <p>Mười hai th&igrave; trong tiếng Anh gi&uacute;p người học diễn đạt ch&iacute;nh x&aacute;c thời gian v&agrave; trạng th&aacute;i của h&agrave;nh động. Để sử dụng th&agrave;nh thạo, cần nắm vững c&ocirc;ng thức, dấu hiệu nhận biết v&agrave; c&aacute;ch d&ugrave;ng của từng th&igrave;. Việc luyện tập thường xuy&ecirc;n th&ocirc;ng qua n&oacute;i, viết v&agrave; l&agrave;m b&agrave;i tập sẽ gi&uacute;p người học sử dụng c&aacute;c th&igrave; một c&aacute;ch tự nhi&ecirc;n v&agrave; ch&iacute;nh x&aacute;c hơn trong giao tiếp cũng như trong học tập.</p>
                    """
                }
            );
        }
    }
}