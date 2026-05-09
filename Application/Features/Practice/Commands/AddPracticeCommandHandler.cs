using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Exam.Events;
using Application.Features.Practice.Events;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Practice;
using Application.Models.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Commands
{
    public sealed class AddPracticeCommandHandler : ICommandHandler<AddPracticeCommand, PracticeDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public AddPracticeCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<PracticeDetailResponse> Handle(AddPracticeCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var practice = new Domain.Entity.Practice
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Title = request.Title,
                TopicId = request.TopicId,
                Description = request.Description
            };
            foreach(var q in request.Questions)
            {
                var questionId = Guid.NewGuid();
                practice.AddPracticeDetail(new Domain.Entity.Question
                {
                    Id = questionId,
                    IsActive = true,
                    Content = q.Content,
                    Explanation = q.Explanation,
                    QuestionTypes = q.QuestionTypes,
                    TopicId = q.TopicId,
                    Answers = q.Answers.Select(a => new Domain.Entity.Answer
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = questionId,
                    }).ToList(),
                });
            }
            await _unitOfWork.PracticeRepository.AddAsync(practice);
            await _eventBus.PublishAsync(new CreatePracticeEvent(
                Practice: new PracticeReadModel(practice.Id, practice.Title, practice.Description, practice.TopicId, practice.CreatedAt, practice.UpdatedAt),
                Questions: practice.PracticeDetails.Select(pd => new QuestionReadModel
                (
                    Id: pd.Question.Id,
                    Content: pd.Question.Content,
                    QuestionTypes: pd.Question.QuestionTypes,
                    Explanation: pd.Question.Explanation ?? "",
                    ImageUrl: pd.Question.ImageUrl,
                    TopicId: pd.Question.TopicId,
                    CreatedAt: pd.Question.CreatedAt,
                    UpdatedAt: pd.Question.UpdatedAt
                )).ToList(),
                Answers: practice.PracticeDetails.SelectMany(pd => pd.Question.Answers.Select(a => new AnswerReadModel
                (
                    Id: a.Id,
                    Content: a.Content,
                    IsCorrect: a.IsCorrect,
                    QuestionId: a.QuestionId,
                    CreatedAt: a.CreatedAt,
                    UpdatedAt: a.UpdatedAt
                ))).ToList(),
                Details: practice.PracticeDetails.Select(pd => new PracticeDetailReadModel
                (
                    PracticeId: practice.Id,
                    QuestionId: pd.Question.Id
                )).ToList()
            ), cancellationToken);
            return new PracticeDetailResponse
            (
                Id: practice.Id,
                Title: practice.Title,
                Description: practice.Description,
                TopicId: practice.TopicId,
                CreatedAt: practice.CreatedAt,
                Questions: practice.PracticeDetails.Select(pd => new QuestionToPracticeResponse
                (
                    Id: pd.Question.Id,
                    Content: pd.Question.Content,
                    Explanation: pd.Question.Explanation ?? "",
                    QuestionTypes: pd.Question.QuestionTypes,
                    Answers: pd.Question.Answers.Select(a => new AnswerToPracticeResponse
                    (
                        Id: a.Id,
                        Content: a.Content,
                        IsCorrect: a.IsCorrect
                    )).ToList()
                )).ToList()
            );
        }
    }
}
