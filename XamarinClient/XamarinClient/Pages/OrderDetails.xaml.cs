using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.ViewModels;

namespace XamarinClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetails : ContentPage
    {
        Order order;
        Driver driver;
        public OrderDetails()
        {
            InitializeComponent();

            
            this.BindingContext = order;
        }

        protected async override void OnAppearing()
        {
            
            await LoadOrder();
            await LoadDriver();
        }

        public async Task LoadOrder()
        {
            lblLoading.Text = "";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["access_token"].ToString());
            
            var response = await httpClient.GetAsync("https://svendproveapi.azurewebsites.net/api/Order/" + Application.Current.Properties["order_id"]);


            if (response.IsSuccessStatusCode)
            {
                using (var content = await response.Content.ReadAsStreamAsync())
                {
                    order = await JsonSerializer.DeserializeAsync<Order>(content);

                    lblOrderId.Text = order.orderId.ToString();
                    lblEmployeeId.Text = order.employeeId.ToString();
                    lblDeliveryAddress.Text = order.deliveryAddress;
                    lblStatus.Text = order.isDelivered.ToString();

                    activityIndicator.IsRunning = false;
                }
            }
            else
            {
                lblLoading.Text = "Fejl med at kontakte API";
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

        private async void btnTakeOrder_Clicked(object sender, EventArgs e)
        {
            order.driverId = driver.driverId;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["access_token"].ToString());
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var content = JsonSerializer.Serialize(order);
            httpClient.BaseAddress = new Uri("https://svendproveapi.azurewebsites.net/api/Order/");
            await httpClient.PutAsync("https://svendproveapi.azurewebsites.net/api/Order/", new StringContent(content));

        }
    }
}