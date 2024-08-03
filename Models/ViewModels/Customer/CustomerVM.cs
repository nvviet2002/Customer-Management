using CustomerManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models.ViewModels.Customer
{
    public class CustomerVM
    {
        [Required]
        [MaxLength(100)]
        public string? Name { set; get; }

        public DateTime? Birthday { set; get; }

        [Required]
        [MaxLength(20)]
        public string? PhoneNumber { set; get; }

        [Required]
        public CustomerSex Sex { set; get; }

        [Required]
        public CustomerAddressVM CustomerAddressVM { set; get; }

    }
}
