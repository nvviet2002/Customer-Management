using CustomerManagement.Data.Entities;
using CustomerManagement.Data;
using CustomerManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Repositories.Classes
{
    public class CustomerAddressRepository : BaseRepository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly AppDbContext _context;
        public CustomerAddressRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CustomerAddress?> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.CustomerAddresss
                            .Include(ca => ca.Ward)
                            .Include(ca => ca.District)
                            .Include(ca => ca.Province)
                            .FirstOrDefaultAsync(ca => ca.CustomerId == customerId);
        }
    }
}
