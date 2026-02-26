using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;

namespace Infrastructure.Repositories.SQLServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public IExamRepository ExamRepository { get;}
        public IQuestionRepository QuestionRepository { get;}
        public IAnswerRepository AnswerRepository { get;}
        public IExamResultRepository ExamResultRepository { get;}
        public IExamCategoryRepository ExamCategoryRepository { get; }
        public IPracticeRepository PracticeRepository { get; }
        public UnitOfWork(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ExamRepository = new ExamRepository(_applicationDbContext, _mapper);
            QuestionRepository = new QuestionRepository(_applicationDbContext, _mapper);
            AnswerRepository = new AnswerRepository(_applicationDbContext, _mapper);
            ExamResultRepository = new ExamResultRepository(_applicationDbContext, _mapper);
            ExamCategoryRepository = new ExamCategoryRepository(_applicationDbContext, _mapper);
            PracticeRepository = new PracticeRepository(_applicationDbContext, _mapper);
        }
        //modify later to support real transaction
        public Task BeginTransactionAsync()
        {
            return Task.CompletedTask;
        }
        public Task CancelAsync()
        {
            // we can use transaction.Commit(), but since this is an UoW, we just silently discard changes if SaveChangesAsync is not called

            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
