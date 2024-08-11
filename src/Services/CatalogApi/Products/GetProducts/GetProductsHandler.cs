
namespace CatalogApi.Products.GetProducts
{
    public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDto>? ProductDtos);
    public class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>().ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            return new GetProductsResult(result.Adapt<IEnumerable<ProductDto>>());
        }
    }
}
