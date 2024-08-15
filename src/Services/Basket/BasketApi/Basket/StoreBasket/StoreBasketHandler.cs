using BasketApi.Data;
using DiscountGrpc;
using FluentValidation;

namespace BasketApi.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            //RuleFor(x => x.Cart).NotNull().WithMessage("{PropertyName} can not be null.!");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("{PropertyName} is required.!");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository, DiscountService.DiscountServiceClient discountServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await ApplyDiscount(request.Cart, cancellationToken);
            return new StoreBasketResult(repository.StoreShoppingCartAsync(request.Cart, cancellationToken).GetAwaiter().GetResult().UserName);
        }

        private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                item.Price -= discountServiceClient.GetCoupon(new GetCouponRequest { ProductName = item.ProductName }).Amount;
            }
        }
    }
}
