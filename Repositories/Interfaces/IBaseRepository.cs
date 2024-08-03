using CustomerManagement.Models.Responses.Pagination;
using System.Linq.Expressions;
using CustomerManagement.Models.ViewModels.Pagination;

namespace CustomerManagement.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetByQuery();

        T? GetById(string id);

        Task<T?> GetByIdAsync(Guid id);

        ICollection<T> GetAll(Expression<Func<T, bool>> expression);
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter
            , Expression<Func<T, dynamic>> orderBy, bool isAcending);

        Task<PaginateResponse<T>> GetAllPaginateAsync(Expression<Func<T, bool>> filter
            , Expression<Func<T, dynamic>> orderBy, bool isAcending, PaginateVM paginateVM);

        T Get(Expression<Func<T, bool>> expression);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entites);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        T Delete(T entity);

        IEnumerable<T> DeleteRange(IEnumerable<T> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
