using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.ViewModels;

namespace XamarinClient.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetails : ContentPage
    {
        public OrderDetails(string id)
        {
            InitializeComponent();

            TestOrder order = new TestOrder() { Id = id, Name = "Test Details", DeliveryAddress = "Ukraine", Status = 1 };
            this.BindingContext = order;
        }
    }
}