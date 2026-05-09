using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Answer;
using Domain.Entity;

namespace Application.Abstractions.Repositories.Read
{
    public interface IAnswerReadRepository
    {
        Task UpsertBulkAsync(IEnumerable<AnswerReadModel> answers);
    }
}
