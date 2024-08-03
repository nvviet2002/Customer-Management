using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Data.Base
{
    public class BaseEntity: IBaseEntity, ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActived { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
