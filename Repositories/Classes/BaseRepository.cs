using CustomerManagement.Data;
using CustomerManagement.Models.Responses.Pagination;
using CustomerManagement.Models.ViewModels.Pagination;
using CustomerManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomerManagement.Repositories.Classes
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetByQuery()
        {
            return _context.Set<T>();
        }

        public T? Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);
        }

        public ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public ICollection<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> filter
            , Expression<Func<T, dynamic>> orderBy, bool ascendingSort)
        {
            var raw = new List<T>();
            if (ascendingSort)
            {
                raw = await _context.Set<T>().Where(filter).OrderBy(orderBy).ToListAsync();
            }
            else
            {
                raw = await _context.Set<T>().Where(filter).OrderByDescending(orderBy).ToListAsync();
            }
            return raw;
        }

        public virtual async Task<PaginateResponse<T>> GetAllPaginateAsync(Expression<Func<T, bool>> filter
            , Expression<Func<T, dynamic>> orderBy, bool ascendingSort, PaginateVM paginateReq)
        {
            var dbSet = _context.Set<T>().Where(filter);
            var totalCount = await dbSet.CountAsync();
            var raw = new List<T>();
            if (ascendingSort)
            {
                raw = await dbSet.OrderBy(orderBy)
                    .Skip((paginateReq.PageNumber - 1) * paginateReq.PageSize)
                    .Take(paginateReq.PageSize).ToListAsync();
            }
            else
            {
                raw = await dbSet.OrderByDescending(orderBy)
                    .Skip((paginateReq.PageNumber - 1) * paginateReq.PageSize)
                    .Take(paginateReq.PageSize).ToListAsync();
            }

            var paginateRes = new PaginateResponse<T>()
            {
                PageNumber = paginateReq.PageNumber,
                PageSize = paginateReq.PageSize,
                TotalCount = totalCount,
                PageCount = raw.Count(),
                TotalPages = (int)Math.Ceiling((decimal)totalCount / paginateReq.PageSize),
                Items = raw,
            };
            return paginateRes;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public T? GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;

        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entites)
        {
            _context.Set<T>().AddRange(entites);
            return entites;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return entities;
        }

        public T Delete(T entity)
        {
            var result = _context.Set<T>().Remove(entity);
            return entity;
        }

        public IEnumerable<T> DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return entities;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
