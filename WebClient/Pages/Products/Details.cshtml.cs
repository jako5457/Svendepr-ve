using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;

namespace WebClient.Pages.Products
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private IApiCaller apiCaller;

        public DetailsModel(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public void OnGet()
        {
        }
    }
}
