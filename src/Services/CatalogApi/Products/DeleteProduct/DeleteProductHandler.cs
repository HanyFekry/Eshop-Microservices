


using CatalogApi.Products.UpdateProduct;
using FluentValidation;

namespace CatalogApi.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool Success);
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
    public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await session.LoadAsync<Product>(request.Id);
            if (entity == null)
                throw new ProductNotFoundException();
            session.Delete(entity);
            await session.SaveChangesAsync();
            return new DeleteProductResult(true);
        }
    }
}
