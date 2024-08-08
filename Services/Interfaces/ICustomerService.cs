using CustomerManagement.Data.Entities;
using CustomerManagement.Models.Responses.Customer;
using CustomerManagement.Models.Responses.Pagination;
using CustomerManagement.Models.ViewModels.Customer;
using CustomerManagement.Models.ViewModels.Pagination;
using System.Linq.Expressions;
using DocumentFormat.OpenXml;
using System.Data;

namespace CustomerManagement.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ICollection<Customer>> GetAllAsync();

        Task<CustomerResponse> CreateAsync(CustomerVM customerVM);

        Task DeleteAsync(Guid id);

        Task<CustomerResponse> GetAsync(Guid id);
        Task<CustomerResponse> UpdateAsync(Guid id, CustomerVM storyReq);

        Task<PaginateResponse<CustomerResponse>> SearchPagingAsync(PaginateVM paginateReq);

        Task<string> ExportExcelAsync(PaginateVM paginateVM);

        Task<string> ImportExcelAsync(DataTable dataTable);

    }
}
