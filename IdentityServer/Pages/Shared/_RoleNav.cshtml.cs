using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Shared
{
    public class _RoleNavModel : PageModel
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public string _role;

        public _RoleNavModel()
        {
            var user = _userManager.GetUserAsync(User).Result;

            _role = _userManager.GetRolesAsync(user).Result.Where(r => r.Contains("Admin")).FirstOrDefault();
        }
    }
}
