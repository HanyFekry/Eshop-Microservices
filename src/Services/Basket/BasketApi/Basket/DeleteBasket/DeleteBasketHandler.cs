using BasketApi.Data;
using FluentValidation;

namespace BasketApi.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool Success);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("{PropertyName} is required.!");
        }
    }
    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return new DeleteBasketResult(
                await repository.DeleteShoppingCartAsync(request.UserName, cancellationToken));
        }
    }
}
