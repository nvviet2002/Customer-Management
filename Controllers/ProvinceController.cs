using CustomerManagement.Commons;
using CustomerManagement.Data.Entities;
using CustomerManagement.Services.Classes;
using CustomerManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CustomerManagement.Controllers
{
    [Route("api/province")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly ILogger<ProvinceController> _logger;
        private readonly IProvinceService _provinceService;

        public ProvinceController(ILogger<ProvinceController> logger, IProvinceService provinceService)
        {
            _logger = logger;
            _provinceService = provinceService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var provinces = await _provinceService.GetAllAsync();

            return Ok(new ApiResponse<ICollection<Province>>(StatusCodes.Status200OK, "Lấy danh sách tỉnh/thành phố thành công!", provinces));
        }
    }

    
}
