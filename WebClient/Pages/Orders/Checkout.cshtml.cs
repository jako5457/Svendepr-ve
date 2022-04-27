using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;
using WebClient.Helpers.Constants;
using WebClient.Helpers.ShoppingCart;

namespace WebClient.Pages.Orders
{
    public class CheckoutModel : PageModel
    {
        private IApiCaller apiCaller;

        public ShoppingCart shoppingCart;

        public CheckoutModel(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task OnGetAsync()
        {
            shoppingCart = new ShoppingCart(HttpContext);
        }
    }
}
