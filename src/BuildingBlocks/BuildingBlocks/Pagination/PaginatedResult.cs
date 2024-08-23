
namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IReadOnlyList<TEntity> data)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public long Count { get; set; } = count;

        public IReadOnlyList<TEntity> Data { get; set; } = data;
    }
}
