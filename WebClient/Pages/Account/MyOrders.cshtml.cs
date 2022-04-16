using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Pagenation;
using WebClient.Helpers.UserHelpers;

namespace WebClient.Pages.Account
{
    public class MyOrdersModel : PageModel
    {
        public Pager Pager { get; set; }
        public void OnGet(int? currentpage)
        {
            if (currentpage != null)
            {
                Pager = new Pager(145, currentpage.Value);
            }
            else
            {
                Pager = new Pager(145);
            }

            // USERID = User?.Claims.GetValueFromClaim("sub");
        }
    }
}
