using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using Xamarin.Forms;
using Xamarin.Forms.Background;
using Xamarin.Forms.Xaml;
using XamarinClient.Helpers.UserHelpers;

namespace XamarinClient
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class MainPage : ContentPage
    {
        OidcClient _client;
        LoginResult _result;


        Lazy<HttpClient> _apiClient = new Lazy<HttpClient>(() => new HttpClient());

        public MainPage()
        {
            InitializeComponent();

            Login.Clicked += Login_Clicked;

            var browser = DependencyService.Get<IBrowser>();

            var options = new OidcClientOptions
            {
                Authority = "https://identityserversvende.azurewebsites.net/",
                ClientId = "native",
                Scope = "openid profile offline_access",
                RedirectUri = "com.companyname.xamarinclient://callback",
                Browser = browser
            };

            _client = new OidcClient(options);
            _apiClient.Value.BaseAddress = new Uri("https://identityserversvende.azurewebsites.net/");
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            _result = await _client.LoginAsync(new LoginRequest());

            if (_result.IsError)
            {
                OutputText.Text = _result.Error;
                return;
            }

            var sb = new StringBuilder(128);
            foreach (var claim in _result.User.Claims)
            {
                sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
                Application.Current.Properties[$"{claim.Type}"] = claim.Value;
            }

            sb.AppendFormat("\n{0}: {1}\n", "refresh token", _result?.RefreshToken ?? "none");
            sb.AppendFormat("\n{0}: {1}\n", "access token", _result.AccessToken);

            OutputText.Text = sb.ToString();

            _apiClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _result?.AccessToken ?? "");

            Login.Text = "Logout";
            Login.Clicked -= Login_Clicked;
            Login.Clicked += LogutButtonClicked;

            MessagingCenter.Send<AppShell>(new AppShell(), "SwitchOn");

            

        }

        async void LogutButtonClicked(object sender, EventArgs e)
        {
            OutputText.Text = "";

            Login.Text = "Login";

            Login.Clicked -= LogutButtonClicked;
            Login.Clicked += Login_Clicked;

            Application.Current.Properties.Clear();

            MessagingCenter.Send<AppShell>(new AppShell(), "SwitchOff");

            await _client.LogoutAsync(new LogoutRequest());


        }
    }
}