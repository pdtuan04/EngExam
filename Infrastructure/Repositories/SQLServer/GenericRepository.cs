using Application.Models.Pagination;
using Application.Repositories;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using Domain.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class GenericRepository<TDomain, TEntity> : IGenericRepository<TDomain>
        where TDomain : class
        where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper _mapper;

        protected GenericRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TDomain entity)
        {
            var dbEntity = _mapper.Map<TEntity>(entity);
            await _dbSet.AddAsync(dbEntity);
        }

        public async Task AddRangeAsync(IEnumerable<TDomain> entities)
        {
            var dbEntities = _mapper.Map<IEnumerable<TEntity>>(entities);
            await _dbSet.AddRangeAsync(dbEntities);
        }

        #region Read

        public async Task<bool> AnyAsync(Expression<Func<TDomain, bool>> filter)
        {
            var dbFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
            return await _dbSet.AnyAsync(dbFilter);
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbSet.AnyAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TDomain, bool>> filter)
        {
            if (filter == null)
                return await _dbSet.CountAsync();

            var dbFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
            return await _dbSet.CountAsync(dbFilter);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<TDomain> GetByIdAsync(object id)
        {
            var dbEntity = await _dbSet.FindAsync(id);
            return _mapper.Map<TDomain>(dbEntity);
        }

        public async Task<PaginationResponse<TResult>> ToPagination<TResult>(
            int pageIndex,
            int pageSize,
            Expression<Func<TDomain, bool>>? filter = null,
            Func<IQueryable<TDomain>, IQueryable<TDomain>>? include = null,
            Expression<Func<TDomain, object>>? orderBy = null,
            bool ascending = true,
            Expression<Func<TDomain, TResult>> selector = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                var dbFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
                query = query.Where(dbFilter);
            }

            Expression<Func<TEntity, object>> dbOrderBy;
            if (orderBy != null)
            {
                dbOrderBy = _mapper.MapExpression<Expression<Func<TEntity, object>>>(orderBy);
            }
            else
            {
                dbOrderBy = x => EF.Property<object>(x, "Id");
            }

            query = ascending ? query.OrderBy(dbOrderBy) : query.OrderByDescending(dbOrderBy);

            var dbSelector = _mapper.MapExpression<Expression<Func<TEntity, TResult>>>(selector);
            var projectedQuery = query.Select(dbSelector);

            var queryExecute = await PaginationDb<TResult>.ToPagedList(projectedQuery, pageIndex, pageSize);

            return new PaginationResponse<TResult>(queryExecute.Items, queryExecute.TotalCount, pageIndex, pageSize);
        }

        public async Task<TDomain?> FirstOrDefaultAsync(
            Expression<Func<TDomain, bool>> filter,
            Func<IQueryable<TDomain>, IQueryable<TDomain>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet.IgnoreQueryFilters().AsNoTracking();

            var dbFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);

            var dbEntity = await query.FirstOrDefaultAsync(dbFilter);

            return _mapper.Map<TDomain>(dbEntity);
        }

        public async Task<TDomain> FirstOrDefaultAsync(
            Expression<Func<TDomain, bool>> filter,
            Expression<Func<TDomain, object>> sort,
            bool ascending = true)
        {
            var query = _dbSet.IgnoreQueryFilters().AsNoTracking();

            var dbFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
            var dbSort = _mapper.MapExpression<Expression<Func<TEntity, object>>>(sort);

            query = query.Where(dbFilter);
            query = ascending ? query.OrderBy(dbSort) : query.OrderByDescending(dbSort);

            var dbEntity = await query.FirstOrDefaultAsync();

            return _mapper.Map<TDomain>(dbEntity);
        }

        #endregion

        #region Update & Delete

        public virtual async Task Update(TDomain entity)
        {
            var dbExam = _mapper.Map<TEntity>(entity);
            _dbSet.Update(dbExam);
        }

        public void UpdateRange(IEnumerable<TDomain> entities)
        {
            var dbEntities = _mapper.Map<IEnumerable<TEntity>>(entities);
            _dbSet.UpdateRange(dbEntities);
        }


        public void DeleteRange(IEnumerable<TDomain> entities)
        {
            var dbEntities = _mapper.Map<IEnumerable<TEntity>>(entities);
            _dbSet.RemoveRange(dbEntities);
        }

        public async Task Delete(object id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        #endregion
    }
}