using LibraryManagement.Data.Entities;
using System.Linq.Expressions;

namespace LibraryManagement.Repository.Contracts
{
    public interface IRepositoryBase<T, TId> where T : BaseEntity<TId>
    {
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(TId id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
    }
}
