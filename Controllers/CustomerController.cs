using CustomerManagement.Commons;
using CustomerManagement.Data.Entities;
using CustomerManagement.Models.Responses.Customer;
using CustomerManagement.Models.Responses.Pagination;
using CustomerManagement.Models.ViewModels.Customer;
using CustomerManagement.Models.ViewModels.Pagination;
using CustomerManagement.Services.Classes;
using CustomerManagement.Services.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.VariantTypes;
using ClosedXML.Excel;
using System.Data;
using CustomerManagement.Enums;

namespace CustomerManagement.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IFileService _fileService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService
            , IFileService fileService)
        {
            _logger = logger;
            _customerService = customerService;
            _fileService = fileService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();

            return Ok(new ApiResponse<ICollection<Customer>>(StatusCodes.Status200OK, "Lấy danh sách khách hàng thành công!", customers));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync(CustomerVM customerVM)
        {
            var customerResponse = await _customerService.CreateAsync(customerVM);

            return Ok(new ApiResponse<CustomerResponse>(StatusCodes.Status200OK, "Tạo khách hàng thành công!", customerResponse));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _customerService.DeleteAsync(id);

            return Ok(new ApiResponse<dynamic>(StatusCodes.Status200OK, "Xóa khách hàng thành công!", null));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var customerResponse = await _customerService.GetAsync(id);

            return Ok(new ApiResponse<CustomerResponse>(StatusCodes.Status200OK, "Lấy thông tin khách hàng thành công!", customerResponse));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, CustomerVM customerVM)
        {
            var customerResponse = await _customerService.UpdateAsync(id, customerVM);

            return Ok(new ApiResponse<CustomerResponse>(StatusCodes.Status200OK, "Cập nhật thông tin khách hàng thành công!", customerResponse));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] PaginateVM paginateVM)
        {
            var customers = await _customerService.SearchPagingAsync(paginateVM);

            return Ok(new ApiResponse<PaginateResponse<CustomerResponse>>(StatusCodes.Status200OK, "Lấy danh sách khách hàng thành công!", customers));
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel([FromQuery] PaginateVM paginateVM)
        {
            string exportPath = await _customerService.ExportExcelAsync(paginateVM);

            if (System.IO.File.Exists(exportPath))
            {
                var bytes = System.IO.File.ReadAllBytes(exportPath);
                return File(bytes, "application/octet-stream", Path.GetFileName(exportPath));
            }

            return Ok(new ApiResponse<dynamic>(StatusCodes.Status500InternalServerError, "Export excel thất bại", null));
        }
    }
}
