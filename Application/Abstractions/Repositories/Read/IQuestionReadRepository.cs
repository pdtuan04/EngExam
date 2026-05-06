using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Question;
using Domain.Entity;

namespace Application.Abstractions.Repositories.Read
{
    public interface IQuestionReadRepository
    {
        Task UpsertAsync(QuestionReadModel question);
        Task UpsertBulkAsync(IEnumerable<QuestionReadModel> questions);
        Task DeleteAsync(Guid id, DateTime deletedAt);
        Task DeleteBulkAsync(IEnumerable<Guid> ids, DateTime deletedAt);
    }
}
