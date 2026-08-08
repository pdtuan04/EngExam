using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entity;
using Infrastructure.Repositories.SQLServer_Read.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class ApplicationDbReadContext : DbContext
    {
        public ApplicationDbReadContext(DbContextOptions<ApplicationDbReadContext> options)
        : base(options)
        {
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ExamDetail> ExamDetails { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<AnswerHistory> AnswerHistories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<ExamCategory> ExamCategories { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<PracticeDetail> PracticeDetails { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<FlashCard> FlashCards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Exam>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<ExamCategory>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Answer>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Practice>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Question>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Topic>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<FlashCard>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<ExamDetail>().HasKey(ed => new { ed.ExamId, ed.QuestionId });
            modelBuilder.Entity<PracticeDetail>().HasKey(pd => new { pd.PracticeId, pd.QuestionId });
            modelBuilder.Entity<Vocabulary>().HasQueryFilter(x => !x.IsDeleted);
            // Seed data when migration
            modelBuilder.SeedingData();
        }
    }
}
