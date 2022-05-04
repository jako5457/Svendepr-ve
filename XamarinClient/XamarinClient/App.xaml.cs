using System;
using Xamarin.Forms;
using Xamarin.Forms.Background;
using Xamarin.Forms.Xaml;

namespace XamarinClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var trackingservice = new TrackingService();
            DependencyService.RegisterSingleton(trackingservice);
            BackgroundAggregatorService.Add(() => trackingservice);

            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
