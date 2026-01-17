using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Application.DTOs.Requests.Question;
using Application.DTOs.Responses;
using Application.Interface;
using Application.Repositories;
using Application.UnitOfWork;
using Domain.Entity;

namespace Application
{
    public class QuestionManager:IQuestionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuestionManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task AddQuestionAsync(CreateQuestionRequest question)
        {
            await _unitOfWork.BeginTransactionAsync();
            var domainQuestion = new Question
            {
                Id = Guid.NewGuid(),
                Content = question.Context,
                QuestionTypes = question.QuestionTypes,
                Explanation = question.Explanation,
                CreatedAt = DateTime.UtcNow,
                TopicId = question.TopicId
            };
            domainQuestion.Answers = question.Answers.Select(a => new Answer
            {
                Id = Guid.NewGuid(),
                Content = a.Context,
                IsCorrect = a.IsCorrect,
                QuestionId = domainQuestion.Id,
            }).ToList();
            await _unitOfWork.QuestionRepository.AddAsync(domainQuestion);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.QuestionRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllAsync()
        {
            var question = await _unitOfWork.QuestionRepository.GetAllAsync();
            return question.Select(q => new QuestionResponse
            {
                Id = q.Id,
                Context = q.Content,
                QuestionTypes = q.QuestionTypes,
                Explanation = q.Explanation,
                Answers = q.Answers.Select(a => new AnswerResponse
                {
                    Id = a.Id,
                    Context = a.Content
                }).ToList()
            }).ToList();
        }

        public async Task<QuestionResponse> GetByIdAsync(Guid id)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);
            var response = new QuestionResponse
            {
                Id = question.Id,
                Context = question.Content,
                QuestionTypes = question.QuestionTypes,
                Explanation = question.Explanation,
                Answers = question.Answers.Select(a => new AnswerResponse
                {
                    Id = a.Id,
                    Context = a.Content
                }).ToList()
            };
            return response;
        }
        public async Task UpdateQuestionAsync(UpdateQuestionRequest question)
        {
            await _unitOfWork.BeginTransactionAsync();
            var domainQuestion = new Question
            {
                Id = question.Id,
                Content = question.Context,
                QuestionTypes = question.QuestionTypes,
                Explanation = question.Explanation,
                UpdatedAt = DateTime.UtcNow,
                TopicId = question.TopicId
            };
            await _unitOfWork.QuestionRepository.UpdateAsync(domainQuestion);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
