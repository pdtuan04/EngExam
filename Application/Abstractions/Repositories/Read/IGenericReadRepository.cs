using Application.Models.Pagination;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IGenericReadRepository<T> where T : class
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<bool> AnyAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync();
        Task<T> GetByIdAsync(object id);
        Task<PaginationResponse<TResult>> ToPagination<TResult>(
            int pageIndex,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            Expression<Func<T, TResult>> selector = null,
            CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(
           Expression<Func<T, bool>> filter,
           Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sort, bool ascending = true);
        Task UpsertAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
