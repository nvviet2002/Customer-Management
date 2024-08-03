using CustomerManagement.Data.Entities;

namespace CustomerManagement.Services.Interfaces
{
    public interface IDistrictService
    {
        Task<ICollection<District>> GetAllAsync();

        Task<ICollection<District>> GetByProvinceCodeAsync(string provinceCode);
    }
}
