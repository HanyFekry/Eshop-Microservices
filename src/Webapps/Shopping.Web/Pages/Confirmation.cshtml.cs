using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnGetOrderSubmittedAsync()
        {
            Message = "Order submitted successfully.";
            return Page();
        }
    }
}
