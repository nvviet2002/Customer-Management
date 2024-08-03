using CustomerManagement.Data.Entities;
using CustomerManagement.Models.Responses.Customer;

namespace CustomerManagement.Repositories.Interfaces
{
    public interface ICustomerAddressRepository : IBaseRepository<CustomerAddress>
    {
        Task<CustomerAddress> GetByCustomerIdAsync(Guid customerId);
    }
}
