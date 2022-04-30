using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Helpers.Api
{
    public class ApiCaller : IApiCaller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApiCaller(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<T?> GetTAsync<T>(HttpMethod method, string url, string accessToken)
        {
            try
            {
                url = _configuration.GetValue<string>("Api:Endpoint") + url;
                var httpRequestMessage = new HttpRequestMessage(method, url);

                httpRequestMessage.Headers.Add("Accept", "application/json");
                //httpRequestMessage.Headers.Add("Content-Type", "application/json");

                var httpclient = _httpClientFactory.CreateClient();

                httpclient.DefaultRequestHeaders.Authorization = !String.IsNullOrEmpty(accessToken) ? null : new AuthenticationHeaderValue("Bearer", accessToken); //No access token authenticationheader = null

                var response = await httpclient.SendAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                {
                    using (var content = await response.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(content);
                    }
                }
            }
            catch (Exception e)
            {
                return default;
            }

            return default;
        }
    }
}
