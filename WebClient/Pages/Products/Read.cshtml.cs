using ApiDataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;
using WebClient.Helpers.Constants;
using WebClient.Helpers.Pagenation;
using WebClient.Helpers.ShoppingCart;

namespace WebClient.Pages.Products
{
    [Authorize]
    public class ReadModel : PageModel
    {
        public ReadModel(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public int Currentpage { get; set; } //Required for pagenation
        private IApiCaller apiCaller { get; set; }

        public List<Product> products { get; set; }

        public async Task OnGetAsync(int? currentpage)
        {
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation
            products = await apiCaller.GetTAsync<List<Product>>("Product/list", await HttpContext.GetTokenAsync("access_token"));
        }

        public async Task<IActionResult> OnPostAdd(string id, string name)
        {
            ShoppingCart shoppingCart = new ShoppingCart(HttpContext);
            shoppingCart.AddItem(new Item { Id = id, amount = 1, name = name});
            return RedirectToPage();
        }
    }
}
