using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Background;

namespace XamarinClient
{
    public class TrackingService : IBackgroundTask
    {
        CancellationTokenSource cts;
        IConnection conn;
        IModel channel;
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

        async Task TrackLocation()
        {

            

            while (true)
            {
                try
                {

                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    var location = await Geolocation.GetLocationAsync(request, cts.Token);

                    if (location != null && Application.Current.Properties["name"] != null)
                    {


                        IBasicProperties props = channel.CreateBasicProperties();
                        props.ContentType = "text/plain";
                        props.DeliveryMode = 2;

                        DateTime foo = DateTime.Now;
                        long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();

                        string message = $"{Application.Current.Properties["sub"]} {location.Latitude} {location.Longitude} {unixTime}";

                        byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("amq.topic", "Track.Location", props, messageBodyBytes);




                        
                    }
                }


                catch (Exception ex)
                {
                    continue;
                }


                await Task.Delay(60000); // 1 minut
            }
        }
    }
}
