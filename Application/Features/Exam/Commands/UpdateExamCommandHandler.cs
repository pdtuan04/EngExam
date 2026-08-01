using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Common.Exceptions;
using Application.Exceptions;
using Application.Features.Exam.Events;
using Application.Features.ExamCategory.Events;
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
    public sealed class UpdateExamCommandHandler : ICommandHandler<UpdateExamCommand, ExamDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public UpdateExamCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<ExamDetailResponse> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var isExist = await _unitOfWork.ExamRepository.GetExamDetail(request.Id) ?? throw new NotFoundException("Exam", request.Id);
            var exam = new Domain.Entity.Exam
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                DurationInMinutes = request.DurationInMinutes,
                ExamCategoryId = request.ExamCategoryId,
                IsActive = request.IsActive ?? true,
                UpdatedAt = now
            };
            var questionsNumber = request.Questions.Count;
            if (questionsNumber < 10)
            {
                throw new InvalidQuesionNumberException();
            }
            foreach (var q in request.Questions)
            {
                var questionId = q.Id == Guid.Empty ? Guid.NewGuid() : q.Id;
                var newQuestion = new Domain.Entity.Question
                {
                    Id = questionId,
                    Content = q.Content,
                    Explanation = q.Explanation,
                    TopicId = q.TopicId,
                    QuestionTypes = q.QuestionTypes,
                    ImageUrl = q.ImageUrl,
                    IsActive = q.IsActive,
                    UpdatedAt = now,
                    Answers = q.Answers.Select(a => new Domain.Entity.Answer
                    {
                        Id = a.Id == Guid.Empty ? Guid.NewGuid() : a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = questionId,
                        UpdatedAt = now
                    }).ToList()
                };
                exam.AddExamDetail(newQuestion, q.Score);
            }
            var totalScore = exam.ExamDetail.Sum(ed => ed.Score);
            if (totalScore != 100)
            {
                throw new InvalidTotalScoreException(totalScore);
            }
            await _unitOfWork.ExamRepository.Update(exam);
            await _eventBus.PublishAsync(new UpdateExamEvent(
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
