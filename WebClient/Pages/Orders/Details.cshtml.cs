using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        public string trackingid;

        public async Task OnGetAsync(string id)
        {
            //Get Order Details

            ViewData["Title"] = $"Details - {id}";

            trackingid = id;
        }
    }
}
