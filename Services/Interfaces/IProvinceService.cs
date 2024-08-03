using CustomerManagement.Data.Entities;

namespace CustomerManagement.Services.Interfaces
{
    public interface IProvinceService
    {
        Task<ICollection<Province>> GetAllAsync();
    }
}
