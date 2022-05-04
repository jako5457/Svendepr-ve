using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using WebClient.Helpers.Api;
using WebClient.Helpers.Api.Models;
using WebClient.Helpers.Pagenation;
using WebClient.Helpers.UserHelpers;

namespace WebClient.Pages.Account
{
    public class MyOrdersModel : PageModel
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public Employee Employee { get; set; }

        private IApiCaller _appicaller;

        public MyOrdersModel(IApiCaller appicaller)
        {
            _appicaller = appicaller;
        }

        public int Currentpage { get; set; } //Required for pagenation
        public async Task OnGetAsync(int? currentpage)
        {
            Currentpage = currentpage.GetValueOrDefault(); //Required for pagenation
            string? accesstoken = await HttpContext.GetTokenAsync("access_token");
            Employee = await _appicaller.GetTAsync<Employee>("Employee/" + HttpUtility.UrlEncode(User.Claims.GetValueFromClaim("email")), accesstoken);
            if (Employee != null)
            {
                Orders = await _appicaller.GetTAsync<List<Order>>("Order/employee/" + Employee.employeeId, accesstoken);
            }
        }
    }
}
