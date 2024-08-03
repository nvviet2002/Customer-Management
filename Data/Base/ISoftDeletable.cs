namespace CustomerManagement.Data.Base
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
