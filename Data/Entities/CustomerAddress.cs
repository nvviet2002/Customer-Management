using CustomerManagement.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Data.Entities
{
    [Table("CustomerAddresss")]
    public class CustomerAddress : BaseEntity
    {
        public string Address { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string? ProvinceCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? WardCode { get; set; }
        public Guid? CustomerId { get; set; }

        [ForeignKey("ProvinceCode")]
        public Province? Province { get; set; }

        [ForeignKey("DistrictCode")]
        public District? District { get; set; }

        [ForeignKey("WardCode")]
        public Ward? Ward { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

    }
}
