using CustomerManagement.Data.Entities;
using CustomerManagement.Services.Interfaces;
using CustomerManagement.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Services.Classes
{
    public class ProvinceService : IProvinceService
    {
        private readonly ILogger<ProvinceService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ProvinceService(ILogger<ProvinceService> logger , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Province>> GetAllAsync()
        {
            ICollection<Province> provinces = await _unitOfWork.GetDbContext().Provinces
                                                        .OrderBy(p=>p.Code).ToListAsync();
            return provinces;
        }

        public async Task<Province?> GetByNameAsync(string name)
        {
            Province? province = await _unitOfWork.GetDbContext().Provinces
                                    .FirstOrDefaultAsync(p=>p.Name.Contains(name));
            return province;
        }
    }
}
