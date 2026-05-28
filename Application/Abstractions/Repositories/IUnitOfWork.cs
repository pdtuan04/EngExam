using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; }
        IExamRepository ExamRepository { get; }
        IExamResultRepository ExamResultRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IExamCategoryRepository ExamCategoryRepository { get; }
        IPracticeRepository PracticeRepository { get; }
        ICourseRepository  CourseRepository { get; }
        ITopicRepository TopicRepository { get; }
        IFlashCardRepository FlashCardRepository { get; }
        IWordRepository WordRepository { get; }
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
