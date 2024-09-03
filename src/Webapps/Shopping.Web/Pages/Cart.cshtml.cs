using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
    {

        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            //await _cartRepository.RemoveItem(cartId, cartItemId);
            logger.LogInformation("remove from cart button clicked.");
            var cart = await basketService.LoadUserBasket();
            cart.Items.RemoveAll(x => x.ProductId == productId);
            await basketService.StoreBasket(new StoreBasketRequest(cart));

            return RedirectToPage();
        }
    }
}