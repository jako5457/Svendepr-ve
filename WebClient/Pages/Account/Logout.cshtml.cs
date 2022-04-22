using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
