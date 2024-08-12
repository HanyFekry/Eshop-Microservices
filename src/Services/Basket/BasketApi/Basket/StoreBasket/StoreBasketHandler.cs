using FluentValidation;

namespace BasketApi.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("{PropertyName} can not be null.!");
            RuleFor(x => x.Cart.UserName).NotNull().NotEmpty().WithMessage("{PropertyName} can not be null.!");
        }
    }
    public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            return new StoreBasketResult("");
        }
    }
}
