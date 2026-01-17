using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Application.Interface;
using Application.Repositories;
using Application.UnitOfWork;
using Domain.Entity;

namespace Application
{
    public class GetRandomExam:IGetRandomExam
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetRandomExam(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<ExamResponse> GetRandomExamAsync()
        {
            var exam = await _unitOfWork.ExamRepository.GetRandomExam();
            var examResponse = new ExamResponse
            {
                Id = exam.Id,
                Description = exam.Description,
                Questions = exam.ExamDetail
                .Select(ed => ed.Question)
                .DistinctBy(q => q.Id)
                .Select(q => new QuestionResponse
                {
                    Id = q.Id,
                    Context = q.Content,
                    QuestionTypes = q.QuestionTypes,
                    Explanation = q.Explanation,
                    Answers = q.Answers
                        .Select(a => new AnswerResponse
                        {
                            Id = a.Id,
                            Context = a.Content
                        }).ToList()
                }).ToList()
            };
            return examResponse;
        }
    }
}
