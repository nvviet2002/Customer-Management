namespace CustomerManagement.Data.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsActived { get; set; }
    }
}
