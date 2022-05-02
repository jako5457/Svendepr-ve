using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.Helpers.Api;
using WebClient.Helpers.Api.Models;
using WebClient.Helpers.Constants;
using WebClient.Helpers.Pagenation;
using WebClient.Helpers.ShoppingCart;
using WebClient.Helpers.UserHelpers;

namespace WebClient.Pages.Products
{

    [Authorize]
    public class ReadModel : PageModel
    {
        public ReadModel(IApiCaller apiCaller)
        {
            this._apiCaller = apiCaller;
        }

        public int Currentpage { get; set; } //Required for pagenation
        private IApiCaller _apiCaller { get; set; }
        public List<Product> Products { get; set; }

        public bool IsAdmin { get; set; }

        [BindProperty]
        public string Confirm { get; set; }

        private string AccesToken { get; set; }

        public async Task OnGetAsync(int? currentpage)
        {
            AccesToken = await HttpContext.GetTokenAsync("access_token");
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation

            if (User.Claims.Where(c => c.Value == "Admin").FirstOrDefault() != null)
            {
                IsAdmin = true;
            }

            Products = await _apiCaller.GetTAsync<List<Product>>("Product/list/", AccesToken);
            //orders = await apiCaller.GetTAsync<ICollection<Order>>("Order", await HttpContext.GetTokenAsync("access_token"));
            //products.AddRange(await apiCaller.GetTAsync<List<Product>>("Product/list/", await HttpContext.GetTokenAsync("access_token")));
        }

        public async Task<IActionResult> OnPostAdd(string id, string name)
        {
            ShoppingCart shoppingCart = new(HttpContext);
            shoppingCart.AddItem(new Item { Id = id, amount = 1, name = name });
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemove(string id)
        {
            if (!String.IsNullOrEmpty(Confirm))
            {
                AccesToken = await HttpContext.GetTokenAsync("access_token");
                Products = await _apiCaller.GetTAsync<List<Product>>("Product/list/", AccesToken);
                Product? deletedproduct = Products.Where(p => p.name == Confirm).FirstOrDefault();

                if (deletedproduct != null)
                {
                    await _apiCaller.DeleteAsync("Product?" + "ProductId=" + id, AccesToken, id);
                }
            }

            return RedirectToPage();
        }
    }
}
