using Geocoding;
using Geocoding.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Web;
using WebClient.Helpers.Api;
using WebClient.Helpers.Api.Models;
using WebClient.Helpers.Constants;
using WebClient.Helpers.Navigation;
using WebClient.Helpers.ShoppingCart;
using WebClient.Helpers.UserHelpers;

namespace WebClient.Pages.Orders
{
    public class CheckoutModel : PageModel
    {

        private IApiCaller _apiCaller;


        public ShoppingCart shoppingCart { get; set; }

        [BindProperty]
        public MyAddress Address { get; set; }

        public CheckoutModel(IApiCaller apiCaller)
        {
            this._apiCaller = apiCaller;
        }

        public async Task OnGetAsync()
        {
            shoppingCart = new ShoppingCart(HttpContext);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            shoppingCart = new ShoppingCart(HttpContext);
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyAhShiSbwyU-t0uvBojMmt29E0lrFYx3jA" };
            var addresses = await geocoder.GeocodeAsync(Address.postcode + " " + Address.shipaddress + " " + Address.address2);

            string jsonstring = JsonSerializer.Serialize(Address);
            string mymail = HttpUtility.UrlEncode(User?.Claims.GetValueFromClaim("email"));
            Employee employee = await _apiCaller.GetTAsync<Employee>("Employee/" + mymail, accessToken);
            if (employee != null)
            {
                List<OrderProductModel> myproducts = new();

                foreach (var item in shoppingCart.ListItems())
                {
                    myproducts.Add(new OrderProductModel { Amount = item.amount, ProductId = item.Id });
                }

                OrderModel myorder = new OrderModel {
                    DeliveryAddress = jsonstring,
                    EmployeeId = employee.employeeId,                    
                    DeliveryLocation = addresses.First().Coordinates.Latitude.ToString().ReplaceComma() + "," + addresses.First().Coordinates.Longitude.ToString().ReplaceComma(),
                    isDelivered = false,
                    driverId = null,
                    Products = myproducts
                };

                bool ting = await _apiCaller.PostAsync<OrderModel>("Order", accessToken, myorder);

                if (ting)
                {
                    return Redirect("/Account/MyOrders");
                }

            }

            return Page();
        }
    }
}
