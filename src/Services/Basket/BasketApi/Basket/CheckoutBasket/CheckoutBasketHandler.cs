using BasketApi.Data;
using BasketApi.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BasketApi.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto Basket) : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool Success);
    public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) :
        ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basketFromDb = await repository.GetShoppingCartAsync(request.Basket.UserName, cancellationToken);
            if (basketFromDb == null)
                return new CheckoutBasketResult(false);

            var basketCheckoutEvent = request.Basket.Adapt<BasketCheckoutEvent>();

            basketCheckoutEvent.TotalPrice = basketFromDb.TotalPrice;

            await publishEndpoint.Publish(basketCheckoutEvent, cancellationToken);
            await repository.DeleteShoppingCartAsync(request.Basket.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
