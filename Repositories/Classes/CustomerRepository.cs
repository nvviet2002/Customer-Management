using CustomerManagement.Data;
using CustomerManagement.Data.Entities;
using CustomerManagement.Exceptions;
using CustomerManagement.Models.Responses.Customer;
using CustomerManagement.Models.Responses.Pagination;
using CustomerManagement.Models.ViewModels.Pagination;
using CustomerManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomerManagement.Repositories.Classes
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
            
        }

        public override async Task<PaginateResponse<Customer>> GetAllPaginateAsync(
                Expression<Func<Customer, bool>> filter
                , Expression<Func<Customer, dynamic>> orderBy
                , bool isAscending, PaginateVM paginateVM)
        {
            var customers = new List<Customer>();
            var rawCustomers =  _context.Customers.Where(filter);
            var rowsTotal = await rawCustomers.CountAsync();

            rawCustomers = rawCustomers.Include(c => c.CustomerAddresss)
                                            .ThenInclude(ca => ca.Ward)
                                            .ThenInclude(ca => ca.District)
                                            .ThenInclude(ca => ca.Province);

            //sort and paginate
            if (isAscending)
            {
                customers = await rawCustomers.OrderBy(orderBy).Skip((paginateVM.PageNumber - 1) * paginateVM.PageSize)
                    .Take(paginateVM.PageSize).ToListAsync();
            }
            else
            {
                customers = await rawCustomers.OrderByDescending(orderBy).Skip((paginateVM.PageNumber - 1) * paginateVM.PageSize)
                    .Take(paginateVM.PageSize).ToListAsync();
            }

            var newPaginateResponse = new PaginateResponse<Customer>()
            {
                TotalCount = rowsTotal,
                PageNumber = paginateVM.PageNumber,
                PageSize = paginateVM.PageSize,
                PageCount = customers.Count,
                TotalPages = (int)Math.Ceiling((decimal)rowsTotal / paginateVM.PageSize),
                Items = customers
            };

            return newPaginateResponse;
        }
    }
}
