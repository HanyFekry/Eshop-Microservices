
namespace BuildingBlocks.Pagination
{
    public class PaginatedRequest(int pageIndex = 0, int pageSize = 10)
    {
        public int PageIndex { get; set; } = pageSize;
        public int PageSize { get; set; } = pageSize;
    }
}
