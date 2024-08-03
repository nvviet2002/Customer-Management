using CustomerManagement.Data.Base;
using CustomerManagement.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Data.Entities
{
    [Table("Customers")]
    public class Customer : BaseEntity
    {
        [MaxLength(100)]
        public string Name { set; get; } = string.Empty;

        public DateTime? Birthday { set; get; } = DateTime.UtcNow;

        [MaxLength(20)]
        public string PhoneNumber { set; get; } = string.Empty;

        public CustomerSex Sex { set; get; } = CustomerSex.Male;

        public ICollection<CustomerAddress>? CustomerAddresss { set; get; }
    }
}
