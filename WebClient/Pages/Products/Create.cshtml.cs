using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;
using WebClient.Helpers.Api.Models;

namespace WebClient.Pages.Products
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductModel MyProduct { get; set; } = new ProductModel();

        [BindProperty]
        public string Error { get; set; }

        private readonly IApiCaller _apicaller; 

        public CreateModel(IApiCaller apicaller)
        {
            _apicaller = apicaller;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            MyProduct.Categories = new List<int>(new int[0]);
            var test = await _apicaller.PostAsync<ProductModel>("Product", await HttpContext.GetTokenAsync("access_token"), MyProduct);
            if (test)
            {
                return Redirect("/Products/Read");
            }
            Error = "Error In Creating Item";
            return Page();
        }
    }
}
