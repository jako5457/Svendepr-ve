using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.Pages;
using XamarinClient.ViewModels;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Orders : ContentPage
    {
        public ObservableCollection<Order> OrderCollection { get; set; }

        public Orders()
        {
            InitializeComponent();

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
                    var Orderlist = Orderresponse.Where(x => x.driverId == null && x.isDelivered == false).ToList();
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

        protected async override void OnAppearing()
        {
            await LoadOrders();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void DetailsButtonClicked (object sender, EventArgs e)
        {
            var order = (Order)((Button)sender).CommandParameter;
            Application.Current.Properties["order_id"] = order.orderId;
            await Navigation.PushAsync(new OrderDetails());
        }
    }
}
