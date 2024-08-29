using Shopping.Web.Models.Basket;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{request.UserName}")]
        Task<GetBasketResponse> GetBasket(GetBasketRequest request);
        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);
        [Post("/basket-service/basket/Checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);
        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);
    }
}
