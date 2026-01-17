using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interface;
using Application.Repositories;

namespace Application
{
    public class GetExam: IGetExamFinder
    {
        public readonly IExamRepository _examRepository;
        public GetExam(IExamRepository examRepository)
        {
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }
        public async Task<ExamResponse?> GetExamByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
            {
                return null;
            }
            return new ExamResponse
            {
                Id = exam.Id,
                Description = exam.Description,
                Questions = exam.ExamDetail
                .Select(ed => ed.Question)
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
        }
        //public async Task<IEnumerable<ExamResponse>> GetExamByCategory(Guid id)
        //{

        //}
    }
}
