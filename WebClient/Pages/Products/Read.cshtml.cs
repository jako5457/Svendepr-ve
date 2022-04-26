using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;
using WebClient.Helpers.Constants;
using WebClient.Helpers.Pagenation;
using WebClient.Helpers.ShoppingCart;

namespace WebClient.Pages.Products
{
    public class ReadModel : PageModel
    {
        public ReadModel(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public int Currentpage { get; set; } //Required for pagenation
        private IApiCaller apiCaller { get; set; }

        public void OnGetAsync(int? currentpage)
        {
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation

        }

        //public async Task<IActionResult> OnPostAdd(int id)
        //{
        //    Cart mycart = HttpContext.Session.Get<Cart>(CartConst.CartSession);
        //    mycart.items.Add( new Item { amount = 1, Id = id.ToString()});
        //    HttpContext.Session.Set<Cart>(CartConst.CartSession, mycart);
        //    return RedirectToPage();
        //}
    }
}
