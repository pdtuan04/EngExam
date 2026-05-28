using Application.Abstractions.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IDbContextTransaction _dbContextTransaction;
        private readonly IMapper _mapper;
        private ICommentRepository commentRepository;
        public ICommentRepository CommentRepository => commentRepository ??= new CommentRepository(_applicationDbContext, _mapper);
        private IExamRepository examRepository;
        public IExamRepository ExamRepository => examRepository ??= new ExamRepository(_applicationDbContext, _mapper);
        private IQuestionRepository questionRepository;
        public IQuestionRepository QuestionRepository => questionRepository ??= new QuestionRepository(_applicationDbContext, _mapper);
        private IAnswerRepository answerRepository;
        public IAnswerRepository AnswerRepository => answerRepository ??= new AnswerRepository(_applicationDbContext, _mapper);
        private IExamResultRepository examResultRepository;
        public IExamResultRepository ExamResultRepository => examResultRepository ??= new ExamResultRepository(_applicationDbContext, _mapper);
        private IExamCategoryRepository examCategoryRepository;
        public IExamCategoryRepository ExamCategoryRepository => examCategoryRepository ??= new ExamCategoryRepository(_applicationDbContext, _mapper);
        private IPracticeRepository practiceRepository;
        public IPracticeRepository PracticeRepository => practiceRepository ??= new PracticeRepository(_applicationDbContext, _mapper);
        private ICourseRepository courseRepository;
        public ICourseRepository CourseRepository => courseRepository ??= new CourseRepository(_applicationDbContext, _mapper);
        private ITopicRepository topicRepository;
        public ITopicRepository TopicRepository => topicRepository ??= new TopicRepository(_applicationDbContext, _mapper);
        private IFlashCardRepository flashCardRepository;
        public IFlashCardRepository FlashCardRepository => flashCardRepository ??= new FlashCardRepository(_applicationDbContext, _mapper);
        private IWordRepository wordRepository;
        public IWordRepository WordRepository => wordRepository ??= new WordRepository(_applicationDbContext, _mapper);
        public UnitOfWork(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task BeginTransactionAsync()
        {
            _dbContextTransaction = await _applicationDbContext.Database.BeginTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _dbContextTransaction.RollbackAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _applicationDbContext.Database.CommitTransactionAsync();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                    _dbContextTransaction.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
