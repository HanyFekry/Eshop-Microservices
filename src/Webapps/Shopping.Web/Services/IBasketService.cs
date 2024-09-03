using Shopping.Web.Models.Basket;
using System.Net;

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
        public async Task<ShoppingCartModel> LoadUserBasket(string userName = "swn")
        {
            // Get Basket If Not Exist Create New Basket with Default Logged In User Name: swn

            ShoppingCartModel basket;

            try
            {
                var getBasketResponse = await GetBasket(new GetBasketRequest(userName));
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    UserName = userName,
                    Items = []
                };
            }

            return basket;
        }
    }
}
