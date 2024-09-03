using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ProductDetailModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductDetailModel> logger)
        : PageModel
    {
        public ProductModel Product { get; set; } = default!;
        [BindProperty]
        public string Color { get; set; } = default!;
        [BindProperty]
        public int Quantity { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProductById(productId);
            Product = response.Product;

            return Page();
        }
        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked.");
            var cart = await basketService.LoadUserBasket();
            var productResponse = await catalogService.GetProductById(productId);
            cart.Items.Add(new()
            {
                ProductId = productResponse.Product.Id,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Color = Color,
                Quantity = Quantity
            });
            await basketService.StoreBasket(new StoreBasketRequest(cart));

            return RedirectToPage("Cart");
        }
    }
}
