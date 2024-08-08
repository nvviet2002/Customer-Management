using AutoMapper;
using CustomerManagement.Commons;
using CustomerManagement.Data.Entities;
using CustomerManagement.Enums;
using CustomerManagement.Exceptions;
using CustomerManagement.Models.Responses.Customer;
using CustomerManagement.Models.Responses.Pagination;
using CustomerManagement.Models.ViewModels.Customer;
using CustomerManagement.Models.ViewModels.Pagination;
using CustomerManagement.Services.Interfaces;
using CustomerManagement.UnitOfWork;
using System.Data;
using CustomerManagement.Extensions;
using System.Linq;
using System.Net;
namespace CustomerManagement.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IProvinceService _provinceService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;
        private readonly ILogger<CustomerService> _logger;


        public CustomerService(ILogger<CustomerService> logger,IMapper mapper, IUnitOfWork unitOfWork
            , IFileService fileService, IProvinceService provinceService, IDistrictService districtService
            , IWardService wardService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;

        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            return await _unitOfWork.CustomerRepository.GetAllAsync();
        }

        public async Task<CustomerResponse> CreateAsync(CustomerVM customerVM)
        {
            try
            {
                Customer newCustomer = _mapper.Map<Customer>(customerVM);
                CustomerAddress newCustomerAddress = _mapper.Map<CustomerAddress>(customerVM.CustomerAddressVM);

                //get province, district, ward
                Province province = await _unitOfWork.GetDbContext().Provinces.FindAsync(newCustomerAddress.ProvinceCode);
                District district = await _unitOfWork.GetDbContext().Districts.FindAsync(newCustomerAddress.DistrictCode);
                Ward ward = await _unitOfWork.GetDbContext().Wards.FindAsync(newCustomerAddress.WardCode);

                //set value
                newCustomerAddress.CustomerId = newCustomer.Id;
                newCustomerAddress.FullAddress = $"{newCustomerAddress.Address}, {ward.Name}, {district.Name}, {province.Name}";

                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
                await _unitOfWork.CustomerAddressRepository.AddAsync(newCustomerAddress);

                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();

                // auto mapper
                var customerResponse = _mapper.Map<CustomerResponse>(newCustomer);
                customerResponse.Address = _mapper.Map<CustomerAddressResponse>(newCustomerAddress);

                return customerResponse;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                throw ex;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException("Khách hàng không tồn tại");
            }

            //find customer address
            var customerAddress = await _unitOfWork.CustomerAddressRepository
                                                    .GetByCustomerIdAsync(id);
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                _unitOfWork.CustomerAddressRepository.Delete(customerAddress);
                _unitOfWork.CustomerRepository.Delete(customer);

                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                throw ex;
            }
        }

        public async Task<CustomerResponse> GetAsync(Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException("Khách hàng không tồn tại");
            }

            var customerAddress = await _unitOfWork.CustomerAddressRepository.GetByCustomerIdAsync(id);

            // auto mapper
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            customerResponse.Address = _mapper.Map<CustomerAddressResponse>(customerAddress);

            return customerResponse;
        }

        public async Task<CustomerResponse> UpdateAsync(Guid id, CustomerVM customerVM)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException("Khách hàng không tồn tại");
            }

            var customerAddress = await _unitOfWork.CustomerAddressRepository.GetByCustomerIdAsync(id);

            customer = _mapper.Map<CustomerVM,Customer>(customerVM, customer);
            customerAddress = _mapper.Map<CustomerAddressVM,CustomerAddress>(customerVM.CustomerAddressVM, customerAddress);

            //get province, district, ward
            Province province = await _unitOfWork.GetDbContext().Provinces.FindAsync(customerAddress.ProvinceCode);
            District district = await _unitOfWork.GetDbContext().Districts.FindAsync(customerAddress.DistrictCode);
            Ward ward = await _unitOfWork.GetDbContext().Wards.FindAsync(customerAddress.WardCode);

            //set value
            customerAddress.FullAddress = $"{customerAddress.Address}, {ward.Name}, {district.Name}, {province.Name}";

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                _unitOfWork.CustomerRepository.Update(customer);
                _unitOfWork.CustomerAddressRepository.Update(customerAddress);

                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();

                return await GetAsync(id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                throw ex;
            }
        }

        public async Task<PaginateResponse<CustomerResponse>> SearchPagingAsync(PaginateVM paginateVM)
        {
            var paginatedCustomers = await _unitOfWork.CustomerRepository.GetAllPaginateAsync(
                                        c => c.IsActived && c.Name.Contains(paginateVM.SearchTerm), c => c.CreatedAt, false, paginateVM);

            List<CustomerResponse> customerResponses = new List<CustomerResponse>();

            foreach (var customer in paginatedCustomers.Items)
            {
                // auto mapper
                var customerResponse = _mapper.Map<CustomerResponse>(customer);
                customerResponse.Address = _mapper.Map<CustomerAddressResponse>(customer.CustomerAddresss.FirstOrDefault());

                customerResponses.Add(customerResponse);
            }

            return new PaginateResponse<CustomerResponse>()
            {
                PageCount = paginatedCustomers.PageCount,
                PageNumber = paginatedCustomers.PageNumber,
                PageSize = paginatedCustomers.PageSize,
                TotalCount = paginatedCustomers.TotalCount,
                TotalPages = paginatedCustomers.TotalPages,
                Items = customerResponses
            };
        }

        public async Task<string> ExportExcelAsync(PaginateVM paginateVM)
        {
            var paginatedCustomers = await SearchPagingAsync(paginateVM);

            var newDatatable = new DataTable();
            newDatatable.Columns.Add("STT", typeof(int));
            newDatatable.Columns.Add("Họ tên", typeof(string));
            newDatatable.Columns.Add("Ngày sinh", typeof(DateTime));
            newDatatable.Columns.Add("Số điện thoại", typeof(string));
            newDatatable.Columns.Add("Giới tính", typeof(string));
            newDatatable.Columns.Add("Số nhà", typeof(string));
            newDatatable.Columns.Add("Xã/phường", typeof(string));
            newDatatable.Columns.Add("Quận/huyện", typeof(string));
            newDatatable.Columns.Add("Tỉnh/thành phố", typeof(string));
            newDatatable.Columns.Add("Tạo lúc", typeof(DateTime));
            newDatatable.Columns.Add("Cập nhật lúc", typeof(DateTime));

            int index = 1;
            foreach (var customer in paginatedCustomers.Items)
            {
                newDatatable.Rows.Add(
                    index++,
                    customer.Name,
                    customer.Birthday.Value.ToShortDateString(),
                    customer.PhoneNumber,
                    customer.Sex.GetEnumDescription(),
                    customer.Address.Address,
                    customer.Address.WardName,
                    customer.Address.DistrictName,
                    customer.Address.ProvinceName,
                    customer.CreatedAt.ToString(),
                    customer.UpdatedAt.ToString()
                    
                );
                
            }

            return await _fileService.ExportToExcel(newDatatable, "export/excel/test.xlsx");
        }

        public async Task<string> ImportExcelAsync(DataTable dataTable)
        {
            try
            {
                //create new
                List<Customer> newCustomers = new List<Customer>();
                List<CustomerAddress> newCustomerAddresss = new List<CustomerAddress>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Province? province = await _provinceService.GetByNameAsync(row[8].ToString());
                    District? district = (await _districtService.GetByProvinceCodeAsync(province.Code))
                                            .FirstOrDefault(d => d.Name.Contains(row[7].ToString()));
                    var wards = await _wardService.GetByDistrictCodeAsync(district.Code);
                    Ward? ward = (await _wardService.GetByDistrictCodeAsync(district.Code))
                                    .FirstOrDefault(w => w.Name.Contains(row[6].ToString()));

                    //get customer sex
                    CustomerSex customerSex;
                    if(string.Equals(CustomerSex.Male.GetEnumDescription(), row[4].ToString()))
                    {
                        customerSex = CustomerSex.Male;
                    }else if (string.Equals(CustomerSex.Female.GetEnumDescription(), row[4].ToString()))
                    {
                        customerSex = CustomerSex.Female;
                    }
                    else
                    {
                        customerSex = CustomerSex.Other;
                    }

                    Customer newCustomer = new Customer()
                    {
                        Name = row[1].ToString(),
                        Birthday = DateTime.Parse(row[2].ToString()),
                        PhoneNumber = row[3].ToString(),
                        Sex = customerSex,
                    };

                    CustomerAddress newCustomerAddress = new CustomerAddress()
                    {
                        Address = row[5].ToString(),
                        ProvinceCode = province.Code,
                        DistrictCode = district.Code,
                        WardCode = ward.Code
                    };

                    //set value
                    newCustomerAddress.CustomerId = newCustomer.Id;
                    newCustomerAddress.FullAddress = $"{newCustomerAddress.Address}, {ward.Name}, {district.Name}, {province.Name}";

                    newCustomers.Add(newCustomer);
                    newCustomerAddresss.Add(newCustomerAddress);


                }

                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.CustomerRepository.AddRangeAsync(newCustomers);
                await _unitOfWork.CustomerAddressRepository.AddRangeAsync(newCustomerAddresss);

                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitAsync();
                return string.Empty;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                throw ex;
            }
        }
        
    }
}
