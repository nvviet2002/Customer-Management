using CustomerManagement.Commons;
using CustomerManagement.Data.Entities;
using CustomerManagement.Services.Classes;
using CustomerManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    [Route("api/district")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly ILogger<DistrictController> _logger;
        private readonly IDistrictService _districtService;

        public DistrictController(ILogger<DistrictController> logger, IDistrictService districtService)
        {
            _logger = logger;
            _districtService = districtService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var districts = await _districtService.GetAllAsync();

            return Ok(new ApiResponse<ICollection<District>>(StatusCodes.Status200OK, "Lấy danh sách quận/huyện thành công!", districts));
        }

        [HttpGet("get-by-province-code/{code}")]
        public async Task<IActionResult> GetByProvinceCodeAsync(string code)
        {
            var districts = await _districtService.GetByProvinceCodeAsync(code);

            return Ok(new ApiResponse<ICollection<District>>(StatusCodes.Status200OK, "Lấy danh sách quận/huyện thành công!", districts));
        }
    }
}
