using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Features.Answer.Events;
using Application.Features.Exam.Events;
using Application.Features.Question.Events;
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
        private readonly IEventBus _eventBus;
        public AddExamCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
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
                Description = request.Description,
                CreatedAt = now,
                UpdatedAt = now,
            };

            foreach (var q in request.Questions)
            {
                var questionId = Guid.NewGuid();
                exam.AddExamDetail(new Domain.Entity.Question
                {
                    Id = questionId,
                    IsActive = true,
                    Content = q.Content,
                    Explanation = q.Explanation,
                    QuestionTypes = q.QuestionTypes,
                    TopicId = q.TopicId,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Answers = q.Answers.Select(a => new Domain.Entity.Answer
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = questionId,
                        CreatedAt = now,
                        UpdatedAt = now,
                    }).ToList(),
                },
                q.Score);
            }
            await _unitOfWork.ExamRepository.AddAsync(exam);
            await _eventBus.PublishAsync(new CreateExamEvent(
                Exam: new ExamReadModel
                (
                    Id: exam.Id,
                    Title: exam.Title,
                    Description: exam.Description,
                    DurationInMinutes: exam.DurationInMinutes,
                    ExamCategoryId: exam.ExamCategoryId,
                    CreatedAt: exam.CreatedAt,
                    UpdatedAt: exam.UpdatedAt
                ),
                Questions: exam.ExamDetail.Select(ed => new QuestionReadModel
                (
                    Id: ed.Question.Id,
                    Content: ed.Question.Content,
                    QuestionTypes: ed.Question.QuestionTypes,
                    Explanation: ed.Question.Explanation ?? "",
                    ImageUrl: ed.Question.ImageUrl,
                    TopicId: ed.Question.TopicId,
                    CreatedAt: ed.Question.CreatedAt,
                    UpdatedAt: ed.Question.UpdatedAt
                )).ToList(),
                Answers: exam.ExamDetail.SelectMany(ed => ed.Question.Answers.Select(a => new AnswerReadModel
                (
                    Id: a.Id,
                    Content: a.Content,
                    IsCorrect: a.IsCorrect,
                    QuestionId: a.QuestionId,
                    CreatedAt: a.CreatedAt,
                    UpdatedAt: a.UpdatedAt
                ))).ToList(),
                ExamDetails: exam.ExamDetail.Select(ed => new ExamDetailReadModel
                (
                    ExamId: exam.Id,
                    QuestionId: ed.QuestionId,
                    Score: ed.Score
                )).ToList()
            ));
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
