


namespace CatalogApi.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Success);
    public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await session.LoadAsync<Product>(request.Id);
            if (entity == null)
                throw new ProductNotFoundException();
            session.Delete(entity);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
