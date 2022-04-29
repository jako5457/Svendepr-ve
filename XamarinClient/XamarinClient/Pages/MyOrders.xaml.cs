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
        public ObservableCollection<Order> OrderList { get; set; }

        public MyOrders(string id)
        {
            InitializeComponent();

            OrderList = new ObservableCollection<Order>
            {

            };

            MyListView.ItemsSource = OrderList;


        }


        private void btnDelivered_Clicked(object sender, EventArgs e)
        {
            var testorder = (Order)((Button)sender).CommandParameter;
            

        }
    }
}