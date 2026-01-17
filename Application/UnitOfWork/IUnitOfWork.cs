using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IExamRepository ExamRepository { get; }
        IExamResultRepository ExamResultRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }

        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CancelAsync();
    }
}
