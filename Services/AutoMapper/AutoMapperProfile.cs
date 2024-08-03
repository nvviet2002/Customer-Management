using AutoMapper;
using CustomerManagement.Data.Entities;
using CustomerManagement.Models.Responses.Customer;
using CustomerManagement.Models.ViewModels.Customer;

namespace CustomerManagement.Services.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {   
            //request
            CreateMap<CustomerVM, Customer>();
            CreateMap<CustomerAddressVM, CustomerAddress>();

            //response
            CreateMap<Customer,CustomerResponse>();
            CreateMap<CustomerAddress, CustomerAddressResponse>()
                .ForMember( car => car.ProvinceName,
                            opt => opt.MapFrom(ca => ca.Province.Name))
                .ForMember(t => t.DistrictName,
                            opt => opt.MapFrom(ca => ca.District.Name))
                .ForMember(t => t.WardName,
                            opt => opt.MapFrom(ca => ca.Ward.Name)); ;
        }
    }
}
