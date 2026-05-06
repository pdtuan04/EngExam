using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
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
            var exam = await _unitOfWork.ExamRepository.GetExamDetail(request.Id) ?? throw new NotFoundException("Exam", request.Id);
            exam.IsActive = request.IsActive ?? exam.IsActive;
            exam.Title = request.Title;
            exam.Description = request.Description;
            exam.DurationInMinutes = request.DurationInMinutes;
            exam.ExamCategoryId = request.ExamCategoryId;
            exam.UpdatedAt = now;
            foreach (var q in request.Questions)
            {
                var existQues = exam.ExamDetail.FirstOrDefault(ed => ed.QuestionId == q.Id);
                if (existQues == null)
                {
                    exam.AddExamDetail(new Domain.Entity.Question
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Explanation = q.Explanation,
                        TopicId = q.TopicId,
                        QuestionTypes = q.QuestionTypes,
                        ImageUrl = q.ImageUrl,
                        IsActive = q.IsActive,
                    }, q.Score);
                }
                else
                {
                    existQues.Question.Content = q.Content;
                    existQues.Question.Explanation = q.Explanation;
                    existQues.Question.TopicId = q.TopicId;
                    existQues.Question.QuestionTypes = q.QuestionTypes;
                    existQues.Question.ImageUrl = q.ImageUrl;
                    existQues.Question.IsActive = q.IsActive;
                    existQues.Score = q.Score;
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
            await _unitOfWork.ExamRepository.Update(exam);
            await _eventBus.PublishAsync(new UpdateExamEvent(
                exam.Id, 
                exam.CreatedAt, 
                exam.UpdatedAt, 
                exam.Title,
                exam.Description,
                exam.DurationInMinutes,
                exam.ExamCategoryId), cancellationToken);
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
