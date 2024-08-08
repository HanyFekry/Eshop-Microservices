
namespace CatalogApi.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Guid Id, string Name, string Description, List<string> Category, double Price);
    public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(request.Id);
            return product.Adapt<GetProductByIdResult>();
        }
    }
}
