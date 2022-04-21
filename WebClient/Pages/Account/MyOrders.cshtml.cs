using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Pagenation;
using WebClient.Helpers.UserHelpers;

namespace WebClient.Pages.Account
{
    public class MyOrdersModel : PageModel
    {
        public int Currentpage { get; set; } //Required for pagenation
        public void OnGet(int? currentpage)
        {
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation
        }
    }
}
