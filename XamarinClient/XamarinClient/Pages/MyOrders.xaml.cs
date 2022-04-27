using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.ViewModels;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrders : ContentPage
    {
        public ObservableCollection<TestOrder> OrderList { get; set; }

        public MyOrders(string id)
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

        private void btnDelivered_Clicked(object sender, EventArgs e)
        {
            var testorder = (TestOrder)((Button)sender).CommandParameter;
            

        }
    }
}