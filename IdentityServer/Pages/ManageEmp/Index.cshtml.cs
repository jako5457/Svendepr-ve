using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Pages.ManageEmp
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public List<ApplicationUser> MyUsers { get; set; }

        public readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async void OnGet()
        {
            MyUsers = _userManager.Users.ToList();
            //var Mylist = await _userManager.GetRolesAsync(MyUsers[3]);
        }
    }
}
