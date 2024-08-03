using CustomerManagement.Data;
using CustomerManagement.Repositories.Classes;
using CustomerManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerManagement.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        private IDbContextTransaction _transaction;

        //define repositories
        public ICustomerRepository CustomerRepository { get; private set; }
        public ICustomerAddressRepository CustomerAddressRepository { get; private set; }


        public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            CustomerRepository = new CustomerRepository(context);
            CustomerAddressRepository = new CustomerAddressRepository(context);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public AppDbContext GetDbContext()
        {
            return _context;
        }
    }
}
