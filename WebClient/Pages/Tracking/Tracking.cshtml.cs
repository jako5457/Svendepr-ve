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

        public Employee Employee { get; set; }

        public Order Order { get; set; }

        public List<Track> Trackings { get; set; }

        public TrackSimple? Destination { get; set; }
        public Track? CurrentPosition { get; set; }

        public string Error { get; set; }

        public TrackingModel(IApiCaller apicaller)
        {
            this._apicaller = apicaller;
        }

        public async Task OnGetAsync(string trackid, string orderid)
        {
            try
            {
                string mymail = HttpUtility.UrlEncode(User?.Claims.GetValueFromClaim("email"));
                string token = await HttpContext.GetTokenAsync("access_token");
                //Employee = await _apicaller.GetTAsync<Employee>("Employee/" + mymail, token);

                Order = await  _apicaller.GetTAsync<Order>("Order/" + orderid, token);

                Trackings = await _apicaller.GetTAsync<List<Track>>("Tracking/" + trackid, token);

                if (Trackings == null)
                {
                    Trackings = new List<Track>();
                }
                else
                {
                    CurrentPosition = Trackings.Last();

                    char[] delimiterchar = { ',' };

                    string[] location = Order.deliveryLocation.Split(delimiterchar, 2);

                    Destination = new TrackSimple();
                    Destination.latitude = location[0];
                    Destination.longitude = location[1];

                    string test1 = CurrentPosition.latitude.ReplaceComma();
                }


            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
    }
}
