using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.ManageEmp
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public ApplicationUser MyUser { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGet(string id)
        {
            MyUser = await _userManager.FindByIdAsync(id);

            //Get Orders From API
            //Updates Functions:
            //Email
            //PhoneNumber
            //Role
            //Password

        }
    }
}
