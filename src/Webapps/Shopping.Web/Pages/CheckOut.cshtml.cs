using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class CheckOutModel(IBasketService basketService, ILogger<CheckOutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            logger.LogInformation("checkout button clicked.");
            if (!ModelState.IsValid)
                return Page();
            Cart = await basketService.LoadUserBasket();
            Order.UserName = "swn";
            Order.CustomerId = Guid.NewGuid();
            Order.TotalPrice = Cart.TotalPrice;

            var response = await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
