namespace BasketApi.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default);
        Task<bool> DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default);
    }
}
