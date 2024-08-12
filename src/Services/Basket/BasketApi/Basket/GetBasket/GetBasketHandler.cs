

namespace BasketApi.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketHandler(IDocumentSession session) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var result = await session.Query<ShoppingCart>()
                .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            if (result == null)
                throw new NotFoundException("Basket", request.UserName);
            return new GetBasketResult(result);
        }
    }
}
