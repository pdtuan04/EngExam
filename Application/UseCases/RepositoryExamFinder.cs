using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Exam.Doing;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interface.Exam;
using Application.Repositories;

namespace Application.UseCases
{
    public class RepositoryExamFinder : IGetExamFinder
    {
        public readonly IExamRepository _examRepository;
        public RepositoryExamFinder(IExamRepository examRepository)
        {
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }
        public async Task<ExamForDoingDTO?> GetExamForDoingAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
            {
                return null;
            }
            return new ExamForDoingDTO
            {
                Id = exam.Id,
                Description = exam.Description ?? "",
                DurationInMinutes = exam.DurationInMinutes,
                Questions = exam.ExamDetail
                .Select(ed => ed.Question)
                .Select(q => new QuestionForDoingDTO
                {
                    Id = q.Id,
                    Context = q.Content,
                    QuestionTypes = q.QuestionTypes,
                    Answers = q.Answers
                        .Select(a => new AnswerForDoingDTO
                        {
                            Id = a.Id,
                            Content = a.Content
                        }).ToList()
                }).ToList()
            };
        }
        public async Task<IEnumerable<ExamSummaryDto>?> GetExamsByCategoryIdAsync(Guid id)
        {
            var exams = await _examRepository.GetExamsByCategoryIdAsync(id);
            var result = exams.Select(exam => new ExamSummaryDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Description = exam.Description
            }).ToList();
            return result;
        }
    }
}
