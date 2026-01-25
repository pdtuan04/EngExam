using Application.DTOs.Exam.Management;
using Application.Interface;
using Application.UnitOfWork;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateNormalExam : ICreateNormalExam
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateNormalExam(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Guid> CreateExam(CreateExamDTO ex)
        {
            try
            {
                var exam = new Exam
                {
                    Id = Guid.NewGuid(),
                    Title = ex.Title,
                    Description = ex.Description,
                    ExamCategoryId = ex.ExamCategoryId,
                };
                foreach (var q in ex.QuestionInCreateExam)
                {
                    exam.AddExamDetail(new Question
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Explanation = q.Explanation,
                        QuestionTypes = q.QuestionTypes,
                        TopicId = q.TopicId,
                        Answers = q.Answers.Select(a => new Answer
                        {
                            Id = a.Id,
                            Content = a.Content,
                            IsCorrect = a.IsCorrect,
                            QuestionId = q.Id,
                        }).ToList(),
                    },
                    q.Score);
                }
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.ExamRepository.AddAsync(exam);
                await _unitOfWork.SaveChangesAsync();
                return exam.Id;
            }
            catch (Exception exc)
            {
                throw new Exception("Create exam failed: " + exc.Message);
            }
        }
    }
}
