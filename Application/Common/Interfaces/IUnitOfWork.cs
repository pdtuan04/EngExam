using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IExamRepository ExamRepository { get; }
        IExamResultRepository ExamResultRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IExamCategoryRepository ExamCategoryRepository { get; }
        IPracticeRepository PracticeRepository { get; }

        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CancelAsync();
    }
}
