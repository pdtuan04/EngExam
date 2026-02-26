using Application.Common.Interfaces;
using Application.Handler.InterfaceHandler;
using Application.Models.Answer;
using Application.Models.Practice;
using Application.Models.Question;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PracticeService : IPracticeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PracticeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<DoPracticeResponse> GetPracticeToTake(Guid id)
        {
            var result = await _unitOfWork.PracticeRepository.GetPracticeToTake(id);
            return new DoPracticeResponse
            {
                Id = result.Id,
                Description = result.Description,
                Questions = result.PracticeDetails.Select(x => new QuestionToPracticeResponse
                {
                    Id = x.QuestionId,
                    Content = x.Question.Content,
                    Explanation = x.Question.Explanation,
                    ImageUrl = x.Question.ImageUrl,
                    QuestionTypes = x.Question.QuestionTypes,
                    Answers = x.Question.Answers.Select(a => new AnswerToPracticeResponse
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };
        }
    }
}
