namespace CustomerManagement.Models.Responses.Customer
{
    public class CustomerAddressResponse
    {
        public string Address { get; set; }
        public string FullAddress { get; set; }
        public string? ProvinceCode { get; set; }
        public string? ProvinceName { get; set; }
        public string? DistrictCode { get; set; }
        public string? DistrictName { get; set; }
        public string? WardCode { get; set; }
        public string? WardName { get; set; }



        public Guid? CustomerId { get; set; }
    }
}
