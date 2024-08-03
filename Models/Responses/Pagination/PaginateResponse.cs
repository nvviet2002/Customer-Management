namespace CustomerManagement.Models.Responses.Pagination
{
    public class PaginateResponse<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageNext => PageNumber + 1 > TotalPages ? TotalPages : PageNumber + 1;
        public int PagePrevious => PageNumber - 1 < 1 ? 1 : PageNumber - 1;
        public ICollection<T>? Items { get; set; }
    }
}
