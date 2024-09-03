using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger)
        : PageModel
    {
        public List<string> CategoryList { get; set; } = default!;
        public IEnumerable<ProductModel> ProductList { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = default!;
        public async Task<IActionResult> OnGet(string categoryName)
        {
            var result = await catalogService.GetProducts();
            CategoryList = result.ProductDtos.SelectMany(x => x.Category).Distinct().ToList();
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = result.ProductDtos;
            }
            else
            {
                var response = await catalogService.GetProductsByCategory(categoryName);
                ProductList = response.ProductDtos;
                SelectedCategory = categoryName;
            }
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
                Color = "Red",
                Quantity = 1
            });
            await basketService.StoreBasket(new StoreBasketRequest(cart));

            return RedirectToPage("Cart");
        }
    }
}
