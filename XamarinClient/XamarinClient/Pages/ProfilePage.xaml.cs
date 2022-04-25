﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            imgQRCode.Source = ImageSource.FromFile("frame.png");

            GetCurrentLocation();

        }
        CancellationTokenSource cts;


        async Task GetCurrentLocation()
        {
            try
            {
                while (true)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    var location = await Geolocation.GetLocationAsync(request, cts.Token);

                    if (location != null)
                    {
                        lblXPosition.Text = location.Longitude.ToString();
                        lblYPosition.Text = location.Latitude.ToString();


                    }
                    await Task.Delay(60000); // 1 minut
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}