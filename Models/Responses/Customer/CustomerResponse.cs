using CustomerManagement.Data.Base;
using CustomerManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Models.Responses.Customer
{
    public class CustomerResponse : IBaseEntity
    {
        public Guid Id { get; set; }

        public string Name { set; get; }

        public DateTime? Birthday { set; get; }

        public string PhoneNumber { set; get; }

        public CustomerSex Sex { set; get; }

        public CustomerAddressResponse Address { set; get; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActived { get; set; }

    }
}
