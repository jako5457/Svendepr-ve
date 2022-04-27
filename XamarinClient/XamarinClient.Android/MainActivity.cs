using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Background.Android;

namespace XamarinClient.Droid
{
    [Activity(Label = "XamarinFormsClient", Icon = "@mipmap/icon", Theme = "@style/MainTheme"
        , MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            BackgroundAggregator.Init(this);

            DependencyService.Register<ChromeCustomTabsBrowser>();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);


            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            CrossCurrentActivity.Current.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}