namespace BasketApi.Exceptions
{
    public class ShoppingCartNotFoundException(string userName) : NotFoundException("Shopping cart", userName);
}
