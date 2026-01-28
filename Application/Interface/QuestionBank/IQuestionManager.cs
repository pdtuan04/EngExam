using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Application.DTOs.Requests.Question;
using Application.DTOs.Responses;

namespace Application.Interface.QuestionBank
{
    public interface IQuestionManager
    {
        Task AddQuestionAsync(CreateQuestionRequest question);
        Task<IEnumerable<QuestionResponse>> GetAllAsync();
        Task<QuestionResponse> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task UpdateQuestionAsync(UpdateQuestionRequest question);
    }
}
