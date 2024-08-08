using CustomerManagement.Data.Entities;
using CustomerManagement.Services.Interfaces;
using CustomerManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Services.Classes
{
    public class WardService : IWardService
    {
        private readonly ILogger<WardService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public WardService(ILogger<WardService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Ward>> GetAllAsync()
        {
            ICollection<Ward> wards = await _unitOfWork.GetDbContext().Wards.ToListAsync();
            return wards;
        }

        public async Task<ICollection<Ward>> GetByDistrictCodeAsync(string districtCode)
        {
            ICollection<Ward> wards = await _unitOfWork.GetDbContext().Wards
                                                        .Where(w => w.DistrictCode == districtCode)
                                                        .OrderBy(w=>w.Code)
                                                        .ToListAsync();
            return wards;
        }
    }
}
