using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.ViewModels;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrders : ContentPage
    {
        public ObservableCollection<Order> OrderCollection { get; set; }
        public Driver driver { get; set; }

        public MyOrders()
        {
            InitializeComponent();



        }

        protected async override void OnAppearing()
        {
            await LoadDriver();
            await LoadOrders();
            
        }

        public async Task LoadOrders()
        {
            lblLoading.Text = "";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["access_token"].ToString());
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await httpClient.GetAsync("https://svendproveapi.azurewebsites.net/api/Order");

            if (response.IsSuccessStatusCode)
            {
                using (var content = await response.Content.ReadAsStreamAsync())
                {
                    OrderCollection = new ObservableCollection<Order>();
                    var Orderresponse = await JsonSerializer.DeserializeAsync<ObservableCollection<Order>>(content);
                    var Orderlist = Orderresponse.Where(x => x.driverId == driver.driverId && x.isDelivered == false).ToList();
                    Orderlist.ForEach(x => OrderCollection.Add(x));
                }
                lblLoading.IsVisible = false;
                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
            }
            else
            {
                lblLoading.Text = "Fejl med at kontakte API";
            }


            MyListView.ItemsSource = OrderCollection;
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


        private async void btnDelivered_Clicked(object sender, EventArgs e)
        {
            var order = (Order)((Button)sender).CommandParameter;

            order.isDelivered = true;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["access_token"].ToString());
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpResponseMessage response = await httpClient.PutAsJsonAsync<Order>("https://svendproveapi.azurewebsites.net/api/Order/" + order.orderId, order);
            response.EnsureSuccessStatusCode();
            LoadOrders();


        }
    }
}