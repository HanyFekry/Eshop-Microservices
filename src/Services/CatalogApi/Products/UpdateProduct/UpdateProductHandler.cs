

namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, double Price,
        string ImageUrl) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Success);
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<Product>();
            var entityFromDb = await session.LoadAsync<Product>(entity.Id);
            if (entityFromDb == null)
                throw new ProductNotFoundException();
            session.Update(entity);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
