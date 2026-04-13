using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Commands
{
    public sealed class AddExamCommandHandler : ICommandHandler<AddExamCommand, ExamDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        AddExamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ExamDetailResponse> Handle(AddExamCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var exam = new Domain.Entity.Exam
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Title = request.Title,
                DurationInMinutes = request.DurationInMinutes,
                ExamCategoryId = request.ExamCategoryId,
            };

            foreach (var q in request.Questions)
            {
                var questionId = Guid.NewGuid();
                exam.AddExamDetail(new Question
                {
                    Id = questionId,
                    IsActive = true,
                    Content = q.Content,
                    Explanation = q.Explanation,
                    QuestionTypes = q.QuestionTypes,
                    TopicId = q.TopicId,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = questionId,
                    }).ToList(),
                },
                q.Score);
            }
            await _unitOfWork.ExamRepository.AddAsync(exam);
            return new ExamDetailResponse
            (
                Id: exam.Id,
                Title: exam.Title,
                Description: exam.Description,
                DurationInMinutes: exam.DurationInMinutes,
                ExamCategoryId: exam.ExamCategoryId,
                CreatedAt: exam.CreatedAt,
                Questions: exam.ExamDetail.Select(ed => new QuestionDetailResponse
                (
                    Id: ed.Question.Id,
                    Content: ed.Question.Content,
                    Explanation: ed.Question.Explanation ?? "",
                    QuestionTypes: ed.Question.QuestionTypes,
                    TopicId: ed.Question.TopicId,
                    Score: ed.Score,
                    CreateAt: ed.Question.CreatedAt,
                    Answers: ed.Question.Answers.Select(a => new AnswerDetailsResponse
                    (
                        Id: a.Id,
                        Content: a.Content,
                        IsCorrect: a.IsCorrect,
                        QuestionId: a.QuestionId
                    )).ToList()
                )).ToList()
            );
        }
    }
}
