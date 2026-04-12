using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        //private readonly string _connectionString;
        //public ApplicationDbContext()
        //{
        //    _connectionString = @"Server=TUAN;Database=ENG;Trusted_Connection=True;TrustServerCertificate=True";
        //}
        //public ApplicationDbContext(string connectionString)
        //{
        //    _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ExamDetail> ExamDetails { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<AnswersHistory> DetailResults { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<ExamCategory> ExamCategories { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<PracticeDetail> PracticeDetails { get; set; }

        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExamDetail>()
                .HasKey(ed => new { ed.ExamId, ed.QuestionId });
            modelBuilder.Entity<ExamDetail>()
                .HasOne(ed => ed.Exam)
                .WithMany(e => e.ExamDetail)
                .HasForeignKey(ed => ed.ExamId);
            modelBuilder.Entity<ExamDetail>()
                .HasOne(ed => ed.Question)
                .WithMany(q => q.ExamDetail)
                .HasForeignKey(ed => ed.QuestionId);

            modelBuilder.Entity<AnswersHistory>()
                .HasKey(ed => new { ed.ExamResultId, ed.QuestionId });
            modelBuilder.Entity<AnswersHistory>()
                .HasOne(ed => ed.ExamResult)
                .WithMany(e => e.AnswerHistory)
                .HasForeignKey(ed => ed.ExamResultId);
            modelBuilder.Entity<AnswersHistory>()
                .HasOne(ed => ed.Question)
                .WithMany(q => q.AnswerHistory)
                .HasForeignKey(ed => ed.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PracticeDetail>()
                .HasKey(pd => new { pd.PracticeId, pd.QuestionId });
            modelBuilder.Entity<PracticeDetail>()
                .HasOne(pd => pd.Practice)
                .WithMany(p => p.PracticeDetails)
                .HasForeignKey(pd => pd.PracticeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PracticeDetail>()
                .HasOne(pd => pd.Question)
                .WithMany(p => p.PracticeDetails)
                .HasForeignKey(pd => pd.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
            // Seed data when migration
            modelBuilder.SeedingData();
        }
    }
}
