using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityServer.Pages.Shared
{
    public class _RoleNavModel : PageModel
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public string _role;

        public _RoleNavModel(UserManager<ApplicationUser> userManager, ClaimsPrincipal currentuser)
        {
            _userManager = userManager;

            var user = _userManager.GetUserAsync(currentuser).Result;

            _role = _userManager.GetRolesAsync(user).Result.Where(r => r.Contains("Admin")).FirstOrDefault();
        }
    }
}
