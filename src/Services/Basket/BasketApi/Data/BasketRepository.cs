namespace BasketApi.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            if (cart == null)
                throw new ShoppingCartNotFoundException(userName);
            return cart;
        }

        public async Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            session.Store<ShoppingCart>(cart);
            await session.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public async Task<bool> DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            if (cart == null)
                throw new ShoppingCartNotFoundException(userName);
            session.Delete(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
