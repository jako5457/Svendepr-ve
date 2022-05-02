using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Api;
using WebClient.Helpers.Api.Models;

namespace WebClient.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        public Order Order { get; set; }

        private IApiCaller _apicaller;

        public DetailsModel(IApiCaller apicaller)
        {
            _apicaller = apicaller;
        }

        public async Task OnGetAsync(string id)
        {
            //Get Order Details

            if (!String.IsNullOrEmpty(id))
            {
                Order = await _apicaller.GetTAsync<Order>("Order/" + id, await HttpContext.GetTokenAsync("access_token"));
            }

            ViewData["Title"] = $"Details - {id}";
        }
    }
}
