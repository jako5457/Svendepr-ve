using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<AppShell>(this, "SwitchOn", (sender) =>
            {
                FlyOutMyProfile.IsEnabled = true;
                FlyOutMyProfile.IsVisible = true;
            });

            MessagingCenter.Subscribe<AppShell>(this, "SwitchOff", (sender) =>
            {
                FlyOutMyProfile.IsEnabled = false;
                FlyOutMyProfile.IsVisible = false;
            });
        }
    }
}