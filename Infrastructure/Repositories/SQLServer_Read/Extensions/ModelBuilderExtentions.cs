using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.Extensions
{
    public static class ModelBuilderExtentions
    {
        public static void SeedingData(this ModelBuilder modelBuilder)
        {
            var admin = new User()
            {
                Id = Guid.Parse("9ae1058d-b602-4025-ab1d-74e7bced8f3b"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM"
            };

            var user = new User()
            {
                Id = Guid.Parse("8d581a98-361e-4333-a651-74e88ef572a4"),
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM"
            };

            modelBuilder.Entity<ExamCategory>().HasData(
                new ExamCategory
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Grammar",
                    Description = "Grammar examination category",
                    ImageUrl = "/uploads/images/category_img.jpg",
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                },
                new ExamCategory
                {
                    Id = Guid.Parse("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"),
                    Name = "Reading",
                    Description = "Reading",
                    ImageUrl = "/uploads/images/category_img.jpg",
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                },
                new ExamCategory
                {
                    Id = Guid.Parse("2af67565-75f7-4511-9b67-3762e917c173"),
                    Name = "Vocabulary",
                    Description = "Vocabulary exam",
                    ImageUrl = "/uploads/images/category_img.jpg",
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                },
                new ExamCategory
                {
                    Id = Guid.Parse("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"),
                    Name = "Listening",
                    Description = "Listening exam",
                    ImageUrl = "/uploads/images/category_img.jpg",
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                }
            );

            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Basic Grammar",
                    Description = "Basic grammar rules",
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Content = "She ___ to school every day.",
                    QuestionTypes = Domain.Enums.QuestionTypes.MultipleChoice,
                    Explanation = "With third person singular, use 'goes'.",
                    TopicId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                }
            );
            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Content = "go",
                    IsCorrect = false,
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                },
                new Answer
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Content = "goes",
                    IsCorrect = true,
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                },
                new Answer
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Content = "going",
                    IsCorrect = false,
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                }
            );

            modelBuilder.Entity<Exam>().HasData(
                new Exam
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Title = "Basic Grammar Test",
                    Description = "Basic Grammar Test",
                    ExamCategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DurationInMinutes = 10,
                    CreatedAt = new DateTime(2026, 01, 01),
                    UpdatedAt = new DateTime(2026, 01, 01),
                    IsActive = true,
                }
            );
            modelBuilder.Entity<ExamDetail>().HasData(
                new ExamDetail
                {
                    ExamId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Score = 1,
                }
            );
        }
    }
}
