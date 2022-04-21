using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;
using WebClient.Helpers.Pagenation;

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

        public async Task OnGetAsync(string kategory, int? currentpage)
        {
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation

            //SomeList = await apiCaller.GetTAsync<List<Root>>(HttpMethod.Get, "/users", "");

            var mystring = "";
        }
    }
}
