using CustomerManagement.Data.Entities;

namespace CustomerManagement.Services.Interfaces
{
    public interface IWardService
    {
        Task<ICollection<Ward>> GetAllAsync();

        Task<ICollection<Ward>> GetByDistrictCodeAsync(string districtCode);
    }
}
