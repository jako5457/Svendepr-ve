using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using WebClient.Helpers.Api;
using WebClient.Helpers.Api.Models;
using WebClient.Helpers.Navigation;
using WebClient.Helpers.UserHelpers;

namespace WebClient.Pages.Tracking
{
    public class TrackingModel : PageModel
    {

        private readonly IApiCaller _apicaller;

        public Employee employee { get; set; }

        public Order order { get; set; }

        public List<Track> Trackings { get; set; }

        public Track Destination { get; set; }
        public Track CurrentPosition { get; set; }

        public string Error { get; set; }

        public TrackingModel(IApiCaller apicaller)
        {
            this._apicaller = apicaller;
        }

        public async Task OnGetAsync(string id)
        {
            try
            {
                id = "11";
                string mymail = HttpUtility.UrlEncode(User?.Claims.GetValueFromClaim("email"));
                string token = await HttpContext.GetTokenAsync("access_token");
                employee = await _apicaller.GetTAsync<Employee>("Employee/" + mymail, token);

                order = await  _apicaller.GetTAsync<Order>("Order/" + id , token);

                Trackings = await _apicaller.GetTAsync<List<Track>>("Tracking/" + order.trackingCode, token);

                CurrentPosition = Trackings.First();

                Destination = Trackings.Last();

                string test1 = CurrentPosition.latitude.ReplaceComma();

            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
    }
}
