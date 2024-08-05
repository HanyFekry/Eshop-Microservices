

namespace CatalogApi.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, double Price,
        string ImageUrl) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //map CreateProductCommand to Product
            //Save Product to database.
            var entity = request.Adapt<Product>();
            session.Store(entity);
            await session.SaveChangesAsync();

            return new CreateProductResult(entity.Id);
        }
    }
}
