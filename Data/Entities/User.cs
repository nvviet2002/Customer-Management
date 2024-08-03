using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CustomerManagement.Data.Base;

namespace CustomerManagement.Data.Entities
{
    [Table("Users")]
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        [MaxLength(100)]
        public string Name { set; get; } = string.Empty;

        public DateTime? Birthday { set; get; } = DateTime.UtcNow;

        [MaxLength(300)]
        public string? Avatar { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActived { get; set; } = true;
        public bool IsDeleted { get; set; } = false;


    }
}
