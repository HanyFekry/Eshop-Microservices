﻿
namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, double Price,
        string ImageUrl) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Success);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 50).WithMessage("{PropertyName} must be between 2 and 50 characters.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
    public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<Product>();
            var entityFromDb = await session.LoadAsync<Product>(entity.Id, cancellationToken);
            if (entityFromDb == null)
                throw new ProductNotFoundException(entity.Id);
            session.Update(entity);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
