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
                    Navigation.PushAsync(new OrderDetails(result.Text));
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