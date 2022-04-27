using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Constants;
using WebClient.Helpers.ShoppingCart;

namespace WebClient.Pages.Shared
{
    public class _WebCartModel : PageModel
    {
        private readonly HttpContext HttpContext;
        public ShoppingCart? shoppingCart { get; set; }
        public _WebCartModel(HttpContext httpContext)
        {
            this.HttpContext = httpContext;
            shoppingCart = new ShoppingCart(HttpContext);
        }
    }
}
