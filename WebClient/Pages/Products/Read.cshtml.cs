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

        public async Task<IActionResult> OnPostAdd(string id, string name)
        {
            ShoppingCart shoppingCart = new ShoppingCart(HttpContext);
            shoppingCart.AddItem(new Item { Id = id, amount = 1, Name = name});
            return RedirectToPage();
        }
    }
}
