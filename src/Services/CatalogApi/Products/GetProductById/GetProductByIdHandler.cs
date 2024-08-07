using CatalogApi.Exceptions;
using CatalogApi.Products.GetProducts;

namespace CatalogApi.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDto? Product);
    public class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) :
        IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdHandler.Handle called with {@request}", request);
            //var product = await session.Query<Product>().FirstOrDefaultAsync(x => x.Id == request.Id) ?? new Product();
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            //if (product == null)
            //    throw new ProductNotFoundException();
            return new GetProductByIdResult(product.Adapt<ProductDto>());
        }
    }
}
