using CustomerManagement.Data.Entities;
using CustomerManagement.Services.Interfaces;
using CustomerManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Services.Classes
{
    public class DistrictService : IDistrictService
    {
        private readonly ILogger<DistrictService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DistrictService(ILogger<DistrictService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<District>> GetAllAsync()
        {
            ICollection<District> districts = await _unitOfWork.GetDbContext().Districts.ToListAsync();
            return districts;
        }

        public async Task<ICollection<District>> GetByProvinceCodeAsync(string provinceCode)
        {
            ICollection<District> districts = await _unitOfWork.GetDbContext().Districts
                                                        .Where(d=>d.ProvinceCode == provinceCode)
                                                        .OrderBy(d=>d.Code)
                                                        .ToListAsync();
            return districts;
        }
    }
}
