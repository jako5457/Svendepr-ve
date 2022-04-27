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
        int id;

        public Scanner()
        {
            InitializeComponent();

            zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(() => {
                btnSeeOrder.IsEnabled = true;
                id = Convert.ToInt32(result.Text);
            });
        }

        private void btnSeeOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OrderDetails(id));
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