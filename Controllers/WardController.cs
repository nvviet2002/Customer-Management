using CustomerManagement.Commons;
using CustomerManagement.Data.Entities;
using CustomerManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    [Route("api/ward")]
    [ApiController]
    public class WardController : ControllerBase
    {
        private readonly ILogger<WardController> _logger;
        private readonly IWardService _wardService;

        public WardController(ILogger<WardController> logger, IWardService wardService)
        {
            _logger = logger;
            _wardService = wardService;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListAsync()
        {
            var wards = await _wardService.GetAllAsync();

            return Ok(new ApiResponse<ICollection<Ward>>(StatusCodes.Status200OK, "Lấy danh sách xã/phường thành công!", wards));
        }

        [HttpGet("get-by-district-code/{code}")]
        public async Task<IActionResult> GetByDistrictCodeAsync(string code)
        {
            var wards = await _wardService.GetByDistrictCodeAsync(code);

            return Ok(new ApiResponse<ICollection<Ward>>(StatusCodes.Status200OK, "Lấy danh sách xã/phường thành công!", wards));
        }
    }
}
