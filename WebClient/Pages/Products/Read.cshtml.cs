using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Pagenation;

namespace WebClient.Pages.Products
{
    public class ReadModel : PageModel
    {
        public int Currentpage { get; set; } //Required for pagenation
        public void OnGet(string kategory, int? currentpage)
        {
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation

        }
    }
}
