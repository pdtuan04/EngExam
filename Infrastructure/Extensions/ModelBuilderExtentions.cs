using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ModelBuilderExtentions
    {
        public static void SeedingData(this ModelBuilder modelBuilder)
        {
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
            List<User> userList = new List<User>()
            {
                admin,
                user,
            };
            modelBuilder.Entity<User>().HasData(userList);
            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>()
                {
                    RoleId = adminRole.Id,
                    UserId = admin.Id
                },
                new IdentityUserRole<Guid>()
                {
                    RoleId = userRole.Id,
                    UserId = user.Id
                }
            };
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);

            modelBuilder.Entity<ExamCategory>().HasData(
                new ExamCategory
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Grammar",
                    Description = "Grammar examination category"
                }
            );

            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Basic Grammar",
                    Description = "Basic grammar rules"
                }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Content = "She ___ to school every day.",
                    QuestionTypes = Domain.Enums.QuestionTypes.MultipleChoice,
                    Explanation = "With third person singular, use 'goes'.",
                    TopicId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                }
            );
            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Content = "go",
                    IsCorrect = false,
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                },
                new Answer
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Content = "goes",
                    IsCorrect = true,
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                },
                new Answer
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Content = "going",
                    IsCorrect = false,
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                }
            );

            modelBuilder.Entity<Exam>().HasData(
                new Exam
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Description = "Basic Grammar Test",
                    ExamCategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                }
            );
            modelBuilder.Entity<ExamDetail>().HasData(
                new ExamDetail
                {
                    ExamId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    QuestionId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Score = 1
                }
            );
        }
    }
}
