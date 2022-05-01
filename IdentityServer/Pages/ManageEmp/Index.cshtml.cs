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
        [BindProperty]
        public string Button { get; set; }

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

        public async Task<IActionResult> OnPost(string id)
        {
            MyUsers = _userManager.Users.ToList();
            switch (Button)
            {
                case "Edit":
                    return Redirect("/ManageEmp/Details/" + id);
                case "Delete":
                    //missing function
                    return Redirect("");
                case "Promote":
                    await _userManager.AddToRoleAsync(MyUsers.Where(c => c.Id == id).FirstOrDefault(), "Admin");
                    return Redirect(Request.Path);
                case "Demote":
                    await _userManager.RemoveFromRoleAsync(MyUsers.Where(c => c.Id == id).FirstOrDefault(), "Admin");
                    await _userManager.AddToRoleAsync(MyUsers.Where(c => c.Id == id).FirstOrDefault(), "User");
                    return Redirect(Request.Path);
                default:
                    return Page();
            }
        }
    }
}
