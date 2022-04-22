using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        public ObservableCollection<TestOrder> OrderList { get; set; }

        public Orders()
        {
            InitializeComponent();

            OrderList = new ObservableCollection<TestOrder>
            {
                new TestOrder{ Id = 1, Name = "Testatatata", DeliveryAddress = "Ukraine", Driver = "", Status = 1},
                new TestOrder{ Id = 2, Name = "Testhmmmm", DeliveryAddress = "Sweden", Driver = "", Status = 1},
                new TestOrder{ Id = 3, Name = "Testttttttt", DeliveryAddress = "Estonia", Driver = "", Status = 1},
                new TestOrder{ Id = 4, Name = "Testman", DeliveryAddress = "Germany", Driver = "", Status = 1},
                new TestOrder{ Id = 5, Name = "Testicle", DeliveryAddress = "Ukraine", Driver = "", Status = 1}
            };

            MyListView.ItemsSource = OrderList;
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
            await Navigation.PushAsync(new OrderDetails());
        }
    }
}
