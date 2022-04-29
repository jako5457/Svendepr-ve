using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.Pages;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scanner : ContentPage
    {

        public Scanner()
        {
            InitializeComponent();

            zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(() => {
                try
                {
                    Application.Current.Properties["order_id"] = result.Text;
                    Navigation.PushAsync(new OrderDetails());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
                
            });
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }
        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}