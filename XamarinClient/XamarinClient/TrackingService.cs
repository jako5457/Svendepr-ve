using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Background;
using XamarinClient.ViewModels;

namespace XamarinClient
{
    public class TrackingService : IBackgroundTask
    {
        CancellationTokenSource cts;
        IConnection conn;
        IModel channel;
        List<Order> orderlist;
        Driver driver;

        public async Task StartJob()
        {
            CreateConnection();
            await TrackLocation();

            
        }

        void CreateConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = "Client";
            factory.Password = "aqXrGg7E";
            factory.VirtualHost = "/";
            factory.HostName = "20.105.251.134";


            conn = factory.CreateConnection();

            channel = conn.CreateModel();
        }

        public async Task LoadOrders()
        {

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["access_token"].ToString());
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await httpClient.GetAsync("https://svendproveapi.azurewebsites.net/api/Order");

            if (response.IsSuccessStatusCode)
            {
                using (var content = await response.Content.ReadAsStreamAsync())
                {
                    var Orderresponse = await JsonSerializer.DeserializeAsync<List<Order>>(content);
                    orderlist = Orderresponse.Where(x => x.driverId == driver.driverId && x.isDelivered == false).ToList();
                }
            }
            else
            {

            }

        }

        public async Task LoadDriver()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["access_token"].ToString());
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await httpClient.GetAsync("https://svendproveapi.azurewebsites.net/api/Driver/");



            if (response.IsSuccessStatusCode)
            {
                using (var content = await response.Content.ReadAsStreamAsync())
                {
                    var driverlist = await JsonSerializer.DeserializeAsync<List<Driver>>(content);
                    driver = driverlist.Where(x => x.name == Application.Current.Properties["name"].ToString()).First();

                }
            }
        }

        async Task TrackLocation()
        {

            

            while (true)
            {
                try
                {
                    LoadDriver();
                    LoadOrders();

                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    var location = await Geolocation.GetLocationAsync(request, cts.Token);

                    if (location != null && Application.Current.Properties["name"] != null)
                    {

                        foreach (Order order in orderlist)
                        {
                            IBasicProperties props = channel.CreateBasicProperties();
                            props.ContentType = "text/plain";
                            props.DeliveryMode = 2;

                            DateTime foo = DateTime.Now;
                            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();

                            string message = $"{order.trackingCode} {location.Latitude} {location.Longitude} {unixTime}";

                            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish("amq.topic", "Track.Location", props, messageBodyBytes);
                        }

                        
                    }
                }


                catch (Exception ex)
                {
                    continue;
                }


                await Task.Delay(10000); // 10 sekunder
            }
        }
    }
}
