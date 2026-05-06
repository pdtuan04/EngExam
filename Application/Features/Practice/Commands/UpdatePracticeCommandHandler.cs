using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Features.Exam.Events;
using Application.Features.ExamCategory.Events;
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
    public sealed class UpdatePracticeCommandHandler : ICommandHandler<UpdatePracticeCommand, PracticeDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public UpdatePracticeCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<PracticeDetailResponse> Handle(UpdatePracticeCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var practice = await _unitOfWork.PracticeRepository.GetPracticeDetail(request.Id) ?? throw new NotFoundException("Practice", request.Id);
            practice.IsActive = request.IsActive ?? practice.IsActive;
            practice.Title = request.Title;
            practice.Description = request.Description;
            practice.UpdatedAt = now;
            practice.TopicId = request.TopicId;
            foreach (var q in request.Questions)
            {
                var existQues = practice.PracticeDetails.FirstOrDefault(pd => pd.QuestionId == q.Id);
                if (existQues == null)
                {
                    practice.AddPracticeDetail(new Domain.Entity.Question
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Explanation = q.Explanation,
                        TopicId = q.TopicId,
                        QuestionTypes = q.QuestionTypes,
                        ImageUrl = q.ImageUrl,
                        IsActive = q.IsActive,
                    });
                }
                else
                {
                    existQues.Question.Content = q.Content;
                    existQues.Question.Explanation = q.Explanation;
                    existQues.Question.TopicId = q.TopicId;
                    existQues.Question.QuestionTypes = q.QuestionTypes;
                    existQues.Question.ImageUrl = q.ImageUrl;
                    existQues.Question.IsActive = q.IsActive;
                    foreach (var a in q.Answers)
                    {
                        var existAns = existQues.Question.Answers.FirstOrDefault(ans => ans.Id == a.Id);
                        if (existAns == null)
                        {
                            existQues.Question.Answers.Add(new Domain.Entity.Answer
                            {
                                Id = a.Id,
                                Content = a.Content,
                                IsCorrect = a.IsCorrect,
                                QuestionId = q.Id,
                                IsActive = true,
                            });
                        }
                        else
                        {
                            existAns.Content = a.Content;
                            existAns.IsCorrect = a.IsCorrect;
                            existAns.IsActive = true;
                        }
                    }
                }

            }
            await _unitOfWork.PracticeRepository.Update(practice);
            await _eventBus.PublishAsync(new UpdatePracticeEvent(practice.Id, practice.Title, practice.Description, practice.CreatedAt, practice.UpdatedAt, practice.TopicId), cancellationToken);
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
