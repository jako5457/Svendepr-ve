using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Constants;
using WebClient.Helpers.ShoppingCart;

namespace WebClient.Pages.Shared
{
    public class _WebCartModel : PageModel
    {
        private HttpContext HttpContext;
        public Cart MyCart { get; set; }
        public _WebCartModel(HttpContext httpContext)
        {
            HttpContext = httpContext;

            if (HttpContext.Session.Get<Cart>(CartConst.CartSession) == null)
            {
                MyCart = new Cart();
                HttpContext.Session.Set<Cart>(CartConst.CartSession, MyCart);
            }
            else
            {
                MyCart = HttpContext.Session.Get<Cart>(CartConst.CartSession);
            }
        }

        public int GetCartItems()
        {
            return MyCart.items.Count();
        }
    }
}
