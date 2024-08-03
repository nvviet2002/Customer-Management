using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models.ViewModels.Customer
{
    public class CustomerAddressVM
    {
        [Required]
        public string? Address { get; set; }

        [Required]
        public string? ProvinceCode { get; set; }

        [Required]
        public string? DistrictCode { get; set; }

        [Required]
        public string? WardCode { get; set; }
    }
}
