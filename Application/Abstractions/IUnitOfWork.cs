using Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IUnitOfWork
    {
        IExamRepository ExamRepository { get; }
        IExamResultRepository ExamResultRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IExamCategoryRepository ExamCategoryRepository { get; }
        IPracticeRepository PracticeRepository { get; }
        ICourseRepository  CourseRepository { get; }
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CancelAsync();
    }
}
