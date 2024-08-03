using CustomerManagement.Data;
using CustomerManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerManagement.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task BeginTransactionAsync();
        Task SaveChangeAsync();
        Task RollBackAsync();
        AppDbContext GetDbContext();

        ICustomerRepository CustomerRepository { get; }
        ICustomerAddressRepository CustomerAddressRepository { get; }
    }
}
