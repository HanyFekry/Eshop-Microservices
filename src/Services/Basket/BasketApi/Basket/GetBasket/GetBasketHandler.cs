

using BasketApi.Data;

namespace BasketApi.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetShoppingCartAsync(request.UserName, cancellationToken);
            return new GetBasketResult(result);
        }
    }
}
